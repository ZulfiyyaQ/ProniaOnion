using ProniaOnion.Application.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.DTOs.Products
{
    public record ProductGetDto(int id, string name, decimal Price, string SKU, string? Description, int CategoryId, IncludeCategoryDto Category);
}
