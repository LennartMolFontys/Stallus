﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallus
{
    public class Address
    {
        string street;
        string number;
        string zipcode;
        string city;

        public string Street { get => street; set => street = value; }
        public string Number { get => number; set => number = value; }
        public string Zipcode { get => zipcode; set => zipcode = value; }
        public string City { get => city; set => city = value; }


        public Address(string street, string number, string zipcode, string city)
        {
            Street = street;
            Number = number;
            Zipcode = zipcode;
            City = city;
        }

        public override string ToString()
        {
            return $"Address: {Street} {Number} \n" +
                    $"        {Zipcode} {City}";
        }
    }
}