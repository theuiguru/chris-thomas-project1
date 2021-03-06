﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantBusiness;
using NLog;

namespace RestaurantPL.Controllers
{
    public class RestaurantsController : Controller
    {
        RestaurantUtility rutility;

        private Logger log;

        public RestaurantsController()
        {
            rutility = new RestaurantUtility();
        }

        // GET: Restaurants
        public ActionResult Index()
        {
            ViewBag.restaurants = rutility.GetRestaurants();
            //ViewBag.avgRatings = rutility.AvgRating(id);
            return View();
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int id)
        {
            var r = rutility.GetRestaurant(id);
            ViewBag.reviews = rutility.GetAllReviews(id);
            //return View(rutility.GetRestaurant(id));
            return View(rutility.GetRestaurant(id));
        }

        // GET: Restaurants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RestaurantBusiness.Restaurant rest)
        {
            try
            {
                // TODO: Add insert logic here
                if(ModelState.IsValid)
                {
                    //business logic
                    rutility.AddRestaurant(rest);
                    return RedirectToAction("Index");
                } else
                {
                    return View(rutility);
                }
            }
            catch
            {
                log = LogManager.GetLogger("errors");
                return View();
            }
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int id)
        {
            return View(rutility.GetRestaurant(id));
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RestaurantBusiness.Restaurant rest)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    //business logic
                    rutility.EditRestaurant(rest, id);
                    return RedirectToAction("Details", new { id = id });
                }
                else
                {
                    log = LogManager.GetLogger("errors");
                    return View(rutility);
                }
            }
            catch(Exception)
            {
                return View();
            }
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int id)
        {
            return View(rutility.GetRestaurant(id));
        }

        // POST: Restaurants/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, RestaurantBusiness.Restaurant rest)
        {
            try
            {
                // TODO: Add delete logic here
                rutility.DeleteRestaurant(id);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                log = LogManager.GetLogger("errors");
                log.Error($"[Restaurants Controller] [Details] Exception thrown: {e.Message}");
                return View();
            }
        }

        public ActionResult SortByRatingAsc()
        {
            return View();
        }

        public ActionResult SortByRatingDesc()
        {
            return View();
        }

        //public ActionResult SortByName()
        //{
        //    return View(rutility.SortByName());
        //}
    }
}