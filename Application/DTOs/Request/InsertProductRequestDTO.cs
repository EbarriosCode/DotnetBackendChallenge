﻿namespace Application.DTOs.Request
{
    public class InsertProductRequestDTO
    {
        public string Name { get; set; }
        public int Status { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
