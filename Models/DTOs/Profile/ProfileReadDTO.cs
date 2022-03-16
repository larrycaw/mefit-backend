﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MeFit.Models.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MeFit.Models.DTOs.Profile
{
    public class ProfileReadDTO
    {
        public int? Weight { get; set; }
        public int? Height { get; set; }
        public int? AddressId { get; set; }
        public int? ProgramId { get; set; }
        public int? WorkoutId { get; set; }
        public int? SetId { get; set; }
        [MaxLength(200)]
        public string MedicalConditions { get; set; }
        [MaxLength(100)]
        public string Disabilities { get; set; }
    }
}