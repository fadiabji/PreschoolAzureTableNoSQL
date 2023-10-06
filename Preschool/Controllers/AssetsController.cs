using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using Preschool.Data;
using Preschool.Extentions;
using Preschool.Models;
using Preschool.Services;
using Preschool.Services.EntitiesServices;

namespace Preschool.Controllers
{
    [Authorize(Roles = ("Admin"))]
    public class AssetsController : Controller
    {
        private readonly IAssetsService _assetsService;
        private readonly IStorgeAssetService _storgeAssetService;

        public AssetsController(IAssetsService assetsService,IStorgeAssetService storgeAssetService)
        {
            _assetsService = assetsService;
            _storgeAssetService = storgeAssetService;
        }

        // GET: Assets
        public async Task<IActionResult> Index()
        {
            //return View(await _assetsService.GetAssets());
            return View( await Task.Run(()=>Conversions.ToAssets( _storgeAssetService.GetAssetEntities())));
        }

        // GET: Assets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var asset = await _assetsService.GetAssetById(id);
            var asset =  await Task.Run(()=>Conversions.ToAsset(_storgeAssetService.GetAssetEntityById(id)));
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // GET: Assets/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Asset asset)
        {
            if (ModelState.IsValid)
            {

                //await Task.Run(() => _assetsService.AddAsset(asset));
                await Task.Run(() => _storgeAssetService.AddAssetToTable(Conversions.ToAssetEntity(asset,asset.Name,"")));
                return RedirectToAction(nameof(Index));
            }
            return View(asset);
        }

        // GET: Assets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var asset = await _assetsService.GetAssetById(id);
            var asset = await Task.Run(() => Conversions.ToAsset(_storgeAssetService.GetAssetEntityById(id)));
            if (asset == null)
            {
                return NotFound();
            }
            return View(asset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Asset asset)
        {
            if (id != asset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //await Task.Run(()=>_assetsService.UpdateAsset(asset));
                    await Task.Run(() => _storgeAssetService.UpdateAssetEntity(Conversions.ToAssetEntity(asset, asset.Name, asset.Id.ToString())));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(asset);
        }

        // GET: Assets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            //var asset = await _assetsService.GetAssetById(id);
            var asset = await Task.Run(() => Conversions.ToAsset(_storgeAssetService.GetAssetEntityById(id)));
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var assets = await _assetsService.GetAssets();
            var assets = await Task.Run(() => Conversions.ToAssets(_storgeAssetService.GetAssetEntities()));
            if (assets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Assets'  is null.");
            }
            var asset = assets.FirstOrDefault(a => a.Id == id);
            if (asset != null)
            {
                //_assetsService.RemoveAsset(asset);
                _storgeAssetService.DeleteAssetEntity(Conversions.ToAssetEntity(asset, asset.Name, asset.Id.ToString()));

            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool AssetExists(int id)
        {
          //return _assetsService.IsExists(id);
          return Conversions.ToAssets(_storgeAssetService.GetAssetEntities()).Any(a => a.Id == id);
        }
    }
}
