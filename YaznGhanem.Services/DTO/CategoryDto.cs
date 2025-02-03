using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    public class CategoryDto
    {
        /// <summary>
        /// Category Id 
        /// </summary>
        public int Id { set; get; }
        
        /// <summary>
        /// Category sort 
        /// </summary>
        public int Sort { set; get; }
        /// <summary>
        /// Show that this category has children or not
        /// </summary>
        public bool HasChildren { set; get; }
        
        /// <summary>
        /// Category name
        /// </summary>
        public string CategoryName { set; get; }
        /// <summary>
        /// Image category path 
        /// </summary>
        [UIHint("Image")]
        public string ImagePath { set; get; }
        /// <summary>
        /// Category path
        /// </summary>
        public string Path { set; get; }
        /// <summary>
        /// list of this category's children
        /// </summary>
        public virtual List<CategoryDto> CategoryChildren { set; get; }
        /// <summary>
        /// The State of this category
        /// </summary>
        public bool IsActive { get; set; }
    }
}
