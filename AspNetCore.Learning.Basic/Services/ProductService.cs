﻿using AspNetCore.Learning.Basic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Learning.Basic.Services
{
    public class ProductService
    {
        public static ProductService Current { get; } = new ProductService();

        public List<ProductDto> Products { get; set; }

        private ProductService()
        {
            Products = new List<ProductDto>
            {
                new ProductDto
                {
                    Id = 1,
                    Name = "牛奶",
                    Price = 2.5f,
                    Materials = new List<MaterialDto>
                    {
                        new MaterialDto
                        {
                            Id = 1,
                            Name = "水"
                        },
                        new MaterialDto
                        {
                            Id = 2,
                            Name = "奶粉"
                        }
                    }
                },
                new ProductDto
                {
                    Id = 2,
                    Name = "面包",
                    Price = 4.5f,
                    Materials = new List<MaterialDto>
                    {
                        new MaterialDto
                        {
                            Id = 3,
                            Name = "面粉"
                        },
                        new MaterialDto
                        {
                            Id = 4,
                            Name = "糖"
                        }
                    }
                },
                new ProductDto
                {
                    Id = 3,
                    Name = "啤酒",
                    Price = 7.5f,
                    Materials = new List<MaterialDto>
                    {
                        new MaterialDto
                        {
                            Id = 5,
                            Name = "麦芽"
                        },
                        new MaterialDto
                        {
                            Id = 6,
                            Name = "地下水"
                        }
                    }
                }
            };
        }
    }
}
