﻿using System.ComponentModel.DataAnnotations;

namespace Zomato.Dto
{
    public class PageRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be 1 or greater.")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100.")]
        public int PageSize { get; set; } = 10;
    }
}
