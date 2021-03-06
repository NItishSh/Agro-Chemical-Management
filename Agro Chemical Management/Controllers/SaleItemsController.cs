﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Agro_Chemical_Management.Models;

namespace Agro_Chemical_Management.Controllers
{
    public class SaleItemsController : Controller
    {
        private AgroChemicalDbEntities db = new AgroChemicalDbEntities();

        // GET: SaleItems
        public async Task<ActionResult> Index()
        {
            var saleItems = db.SaleItems.Include(s => s.Product).Include(s => s.Sale);
            return View(await saleItems.ToListAsync());
        }

        // GET: SaleItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleItem saleItem = await db.SaleItems.FindAsync(id);
            if (saleItem == null)
            {
                return HttpNotFound();
            }
            return View(saleItem);
        }

        // GET: SaleItems/Create
        public ActionResult Create()
        {
            ViewBag.ProductCode = new SelectList(db.Products, "ProductCode", "Name");
            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "CustomerName");
            return View();
        }

        // POST: SaleItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,SaleId,ProductCode,Quantity,Price,TaxAmount,Total")] SaleItem saleItem)
        {
            if (ModelState.IsValid)
            {
                db.SaleItems.Add(saleItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCode = new SelectList(db.Products, "ProductCode", "Name", saleItem.ProductCode);
            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "CustomerName", saleItem.SaleId);
            return View(saleItem);
        }

        // GET: SaleItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleItem saleItem = await db.SaleItems.FindAsync(id);
            if (saleItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCode = new SelectList(db.Products, "ProductCode", "Name", saleItem.ProductCode);
            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "CustomerName", saleItem.SaleId);
            return View(saleItem);
        }

        // POST: SaleItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SaleId,ProductCode,Quantity,Price,TaxAmount,Total")] SaleItem saleItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCode = new SelectList(db.Products, "ProductCode", "Name", saleItem.ProductCode);
            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "CustomerName", saleItem.SaleId);
            return View(saleItem);
        }

        // GET: SaleItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleItem saleItem = await db.SaleItems.FindAsync(id);
            if (saleItem == null)
            {
                return HttpNotFound();
            }
            return View(saleItem);
        }

        // POST: SaleItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SaleItem saleItem = await db.SaleItems.FindAsync(id);
            db.SaleItems.Remove(saleItem);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
