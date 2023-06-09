﻿using EShop.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Web.Models.Domain
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }

        //za OneToOne relacijata megju ShoppingCart i User
        
        //Nadvoresen kluc do userot na koj pripagja ovaa shopping katicka
        public string OwnerId { get; set; }
        public EShopApplicationUser Owner { get; set; }
        //


        public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
    }
}
