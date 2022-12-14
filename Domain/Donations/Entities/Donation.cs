/* Donation module - Asociación Lista y Valiente
 * Collaborators:
 * - Jimena Gdur
 * - Rodrigo Li
 * - Daniela Murillo
 * - Milen Rodriguez
 * - Jorim Wilson
 * 
 * - Summary: Implementation of Donation class
 *   Stores all the donation's information.
 */

/* Project includes */
using Domain.Core.Entities;
using Domain.Core.Exceptions;
using Domain.Core.Helpers;
using Domain.Core.ValueObjects;
using Domain.Products.Entities;
/* System includes */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Donations.Entities
{
	public class Donation : AggregateRoot
	{
		/**  Attributes **/

		// Basic attributes
		public DateTime CreationDate { get; set; }
		public string Status { get; set; }
		public string Province { get; set; }
		public string County { get; set; }
		public string District { get; set; }
		public string ExactLocation { get; set; }
		public string Description { get; set; }
		public string DonorId { get; set; }

		public const int MaxDonationSize = 10;

		// Other attributes
		private readonly List<Product> _products;
		public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

		/**  Methods **/

		// Basic constructor for Donation
		public Donation(int id, string province, string county,
			string district, string exactLocation, string description, 
			string donorId)
		{
			Id = id;
			CreationDate = DateTime.Today;
			Status = "A";
			Province = province;
			County = county;
			District = district;
			ExactLocation = exactLocation;
			Description = description;
			_products = new List<Product>();
			DonorId = donorId;
		}

		//new constructor for tests
		public Donation( string province, string county,
			string district, string exactLocation, string description,
			string donorId)
		{
			
			CreationDate = DateTime.Today;
			Status = "A";
			Province = province;
			County = county;
			District = district;
			ExactLocation = exactLocation;
			Description = description;
			_products = new List<Product>();
			DonorId = donorId;
		}

		// Empty constructor for EFCore
		public Donation()
		{
			Id = 0;
			CreationDate = DateTime.Today;
			Status = "A";
			Province = "";
			County = "";
			District = "";
			_products = new List<Product>();
			ExactLocation = "";
			Description = "";
			DonorId = "";
		}

		// Adds a product to this donation
		public void AddProductToDonation(Product product)
		{
			if (_products.Count >= MaxDonationSize)
				throw new DomainException
					("Donation is at it's maximum capacity.");
			if (_products.Exists(p => p == product))
				throw new DomainException
					("Product is already in the donation.");
			if (_products.Exists(p => p.Name == product.Name))
				throw new DomainException
					("A product with that name is " +
						"already registered in the donation.");

			_products.Add(product);
			product.AssignDonation(this);
		}

		// Clears product list
		public void Clear() {
			_products.Clear();
		}

		// Modifies a product using the old product to locate it in the list
		public void ModifyProduct(Product newProduct, Product oldProduct) {
			if (_products.Exists(p => p == oldProduct)) {
				Donation donation = _products.Find
					(p => p == oldProduct).Donation;
				_products.Find(p => p == oldProduct).Id =
					newProduct.Id;
				_products.Find(p => p == oldProduct).Name = 
					newProduct.Name;
				_products.Find(p => p == oldProduct).Brand = 
					newProduct.Brand;
				_products.Find(p => p == oldProduct).ExpirationDate = 
					newProduct.ExpirationDate;
				_products.Find(p => p == oldProduct).Weight = 
					newProduct.Weight;
				_products.Find(p => p == oldProduct).Quantity = 
					newProduct.Quantity;
				_products.Find(p => p == oldProduct).FoodType = 
					newProduct.FoodType;
				_products.Find(p => p == oldProduct).ProductType = 
					newProduct.ProductType;
				_products.Find(p => p == oldProduct).ClearRestrictions();
				foreach (var restriction in newProduct.Restrictions) {
					_products.Find(p => p == oldProduct).
						AddRestrictionToProduct(restriction);
				}
			}
		}

		// Removes a product from a donation
		public void RemoveProductFromDonation(Product product, bool RemoveDonation = false) {
			if (_products.Exists(p => p == product)) {
				_products.Remove(product);
				if (RemoveDonation) { 
					product.AssignDonation(null);
				}
				
			}
		}
	}
}
