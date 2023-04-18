﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Models
{
    public partial class student
    {
        [Key]
        public int id { get; set; }
        [StringLength(50)]
        [MinLength(4)]
        public string name { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "*")]
       
        [EmailAddress(ErrorMessage = "Email not valid")]
        public string email { get; set; }
        [StringLength(20)]
        [Required(ErrorMessage = "*")]
        //  [RegularExpression("[a-z0-9]+@[a-z]+{2,3}")]
        public string password { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "*")]
        [Compare("password", ErrorMessage = "password not match")]
        public string confirm_password { get; set; }

      
        //[Range(10, 25, ErrorMessage = "age must between 10 and 25 years old")]
        //[Required(ErrorMessage = "*")]
        public int? age { get; set; }
        [InverseProperty("student")]
        public virtual ICollection<book> books { get; set; }

    }
}