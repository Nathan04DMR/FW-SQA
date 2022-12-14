/* Donation module - Asociación Lista y Valiente
 * Collaborators:
 * - Jimena Gdur
 * - Rodrigo Li
 * - Daniela Murillo
 * - Milen Rodriguez
 * - Jorim Wilson
 * 
 * - Summary: Implementation of BelongsTo class
 *   Class for the relationship in between product and donation/campaign
 */

/* Project includes */
using Domain.Core.Entities;
using Domain.Core.Exceptions;
using Domain.Core.ValueObjects;
using Domain.Donations.Entities;
using Domain.Orders.Entities;
/* System includes */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Products.Entities
{
    public class BelongsTo
    {
        /**  Attributes **/

        // Basic attributes
        public Donation? Donation { get; set; }
        public int DonationId { get; set; }
        public Product? Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        /**  Methods **/

        // Basic constructor for BelongsTo
        public BelongsTo(int quantity)
        {
            DonationId = 0;
            Donation = null;
            ProductId = 0;
            Product = null;
            Quantity = quantity;
        }

        // Empty constructor for EFCore
        public BelongsTo()
        {
            DonationId = 0;
            Donation = null;
            ProductId = 0;
            Product = null;
            Quantity = 0;
        }

        // Assigns the donation to which the product belongs
        public void AssignDonation(Donation? donation)
        {
            Donation = donation;
            if (Donation != null)
            {
                DonationId = donation.Id;
            }

        }
        
        // Assigns the current product
        public void AssignProduct(Product? product)
        {
            Product = product;
            if (Product != null)
            {
                ProductId = product.Id;
            }

        }
    }
}