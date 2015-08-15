using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Walltage.Domain.Entities;
using Walltage.Domain.Repositories;

namespace Walltage.WebUI.Infrastructures
{
    public class Utility
    {
        public static List<SelectListItem> GetCategories()
        {
            return CategoryRepo.Instance.GetAll().Select(x => new SelectListItem
                {
                    Selected = false,
                    Text = x.Name,
                    Value = x.CategoryId.ToString()
                }).ToList();
        }

        public static List<SelectListItem> GetResolutions()
        {
            return ResolutionRepo.Instance.GetAll().Select(x => new SelectListItem
                {
                    Selected = false,
                    Text = x.Name,
                    Value = x.ResolutionId.ToString()
                }).ToList();
        }



    }
}