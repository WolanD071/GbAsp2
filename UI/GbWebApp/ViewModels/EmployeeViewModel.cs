using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using GbWebApp.Domain.Entities;

namespace GbWebApp.ViewModels
{
    public class EmployeeViewModel : IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "this is required field!")]
        [Display(Name = "- emp's surname -")]
        [StringLength(maximumLength: 20, MinimumLength = 2, ErrorMessage = "the length of the surname must be from 2 to 20 characters!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "this is required field!")]
        [Display(Name = "- emp's name -")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "this is required field!")]
        [Display(Name = "- emp's midname -")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "this is required field!")]
        public string Snils { get; set; }

        [Required(ErrorMessage = "this is required field!")]
        public float Salary { get; set; }

        [Required(ErrorMessage = "this is required field!")]
        [Range(minimum: 18, maximum: 65, ErrorMessage = "the age must be from 18 to 65 y.o!")]
        public int Age { get; set; }

        public EmployeeViewModel(Employee emp)
        {
            Id = emp.Id;
            Age = emp.Age;
            Snils = emp.Snils;
            Salary = emp.Salary;
            LastName = emp.LastName;
            FirstName = emp.FirstName;
            Patronymic = emp.Patronymic;
        }

        public EmployeeViewModel()
        {
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            yield return ValidationResult.Success;
            //yield return new ValidationResult("error!");
            //yield return new ValidationResult("error!", new[] { nameof(LastName), nameof(FirstName) });
        }
    }
}
