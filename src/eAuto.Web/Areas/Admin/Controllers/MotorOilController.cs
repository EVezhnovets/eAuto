using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eAuto.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebConstants.AdminRole)]
    public class MotorOilController : Controller
    {
        private readonly IAppLogger<MotorOilController> _logger;
		private readonly IImageManager _imageManager;

		private readonly IMotorOilService _motorOilService;
        private readonly IProductBrandService _productBrandService;

        public MotorOilController(
            IMotorOilService motorOilService,
			IProductBrandService productBrandService,
			IAppLogger<MotorOilController> logger,
			IImageManager imageManager)
        {
            _motorOilService = motorOilService;
            _productBrandService = productBrandService;
			_logger = logger;
            _imageManager = imageManager;
		}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
			try
			{
				var iMotorOilsList = await _motorOilService.GetMotorOilModelsAsync();
                var motorOilsList = iMotorOilsList
                    .Select(i => new MotorOilViewModel
                    {
                        MotorOilId = i.MotorOilId,
                        Name = i.Name,
                        PictureUrl = i.PictureUrl,
                        Price = i.Price,
                        Viscosity = i.Viscosity,
                        Composition = i.Composition,
                        Volume = i.Volume,
                        ProductBrandId = i.ProductBrandId,
                        ProductBrand = i.ProductBrand
                    })
                    .OrderBy(i => i.MotorOilId)
                    .ThenBy(i => i.ProductBrand)
                    .ThenBy(i => i.Name);
 
                return View(motorOilsList);
			}
			catch (GenericNotFoundException<MotorOilController> ex)
			{
				_logger.LogError(ex, ex.Message);
				return NotFound(ex.Message);
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
			}
            
        }

		#region Create

		[HttpGet]
		public async Task<IActionResult> Create()
		{
            try
            {
                var productBrandsList = await _productBrandService.GetProductBrandModelsAsync();

                MotorOilCreateViewModel createViewModel = new()
                {
                    MotorOilVModel = new(),
                    ProductBrands = productBrandsList.Select(s => new SelectListItem { Value = s.ProductBrandId.ToString(), Text = s.Name })
                                       .OrderBy(s => s.Text)
				};
				return View(createViewModel);
            }
            catch (GenericNotFoundException<MotorOilController> ex)
            {
				_logger.LogError(ex, ex.Message);
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Create(MotorOilCreateViewModel viewModel)
		    {
			IMotorOil motorOil;
			var files = HttpContext.Request.Form.Files;
			try
			{
				if (files.Count > 0)
                {
					_imageManager.UploadFiles(files, WebConstants.MotorOilsImages);
					viewModel.MotorOilVModel!.PictureUrl = string.Concat(WebConstants.MotorOilsImages, _imageManager.FilesName.FirstOrDefault());
				}
				if (ModelState.IsValid)
                {
					motorOil = _motorOilService.CreateMotorOilDomainModel();

                    motorOil.MotorOilId = viewModel.MotorOilVModel!.MotorOilId;
                    motorOil.Name = viewModel.MotorOilVModel!.Name;
                    motorOil.PictureUrl = viewModel.MotorOilVModel.PictureUrl;
                    motorOil.Price = viewModel.MotorOilVModel.Price;
                    motorOil.Viscosity = viewModel.MotorOilVModel.Viscosity;
                    motorOil.Composition = viewModel.MotorOilVModel.Composition;
                    motorOil.Volume = viewModel.MotorOilVModel.Volume;
                    motorOil.ProductBrandId = viewModel.MotorOilVModel.ProductBrandId;
                    motorOil.Save();

                    TempData["Success"] = "MotorOil created successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
			catch (GenericNotFoundException<MotorOilController> ex)
			{
				_logger.LogError(ex, ex.Message);
				return RedirectToAction("Index");
			}
        }
        #endregion

        #region Edit

        [HttpGet]
        public async Task <IActionResult> Edit(int id)
        {
            try
            {
                var productBrandsIList = await _productBrandService.GetProductBrandModelsAsync();
                var productBrandsList= productBrandsIList
					.Select(b => new ProductBrandViewModel()
                    {
                        ProductBrandId = b.ProductBrandId,
                        Name = b.Name
                    }).ToList();

                var viewModel = _motorOilService.GetMotorOilModel(id);

				MotorOilCreateViewModel createViewModel = new()
                {
                    MotorOilVModel = new MotorOilViewModel
					{
                        MotorOilId = viewModel.MotorOilId,
						Name = viewModel.Name,
                        PictureUrl = viewModel.PictureUrl,
                        Price = viewModel.Price,
						Viscosity = viewModel.Viscosity,
                        Composition = viewModel.Composition,
                        Volume = viewModel.Volume,
                        ProductBrandId = viewModel.ProductBrandId,
                        ProductBrand = viewModel.ProductBrand
					},
                    ProductBrands = productBrandsList.Select(b => new SelectListItem { Value = b.ProductBrandId.ToString(), Text = b.Name }),
                };

                return View(createViewModel);
            }

            catch (GenericNotFoundException<MotorOilController> ex)
            {
				_logger.LogError(ex, ex.Message);
				return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MotorOilCreateViewModel viewModel)
        {
			IMotorOil motorOil;
			try
			{
				var files = HttpContext.Request.Form.Files;

				if (files.Count > 0)
				{
					_imageManager.RemoveFile(WebConstants.MotorOilsImages, viewModel!.MotorOilVModel!.PictureUrl!);
					_imageManager.UploadFiles(files, WebConstants.MotorOilsImages);
					viewModel.MotorOilVModel.PictureUrl = string.Concat(WebConstants.MotorOilsImages, _imageManager.FilesName.FirstOrDefault());
				}
				if (ModelState.IsValid)
                {
					motorOil = _motorOilService.GetMotorOilModel(viewModel.MotorOilVModel!.MotorOilId);
                    motorOil.Name = viewModel.MotorOilVModel.Name!;
                    motorOil.PictureUrl = viewModel.MotorOilVModel.PictureUrl!;
                    motorOil.Price = viewModel.MotorOilVModel.Price;
                    motorOil.ProductBrandId = viewModel.MotorOilVModel.ProductBrandId;
                    motorOil.ProductBrand = _productBrandService.GetProductBrandModel(viewModel.MotorOilVModel.ProductBrandId).Name!.ToString();
                    motorOil.Viscosity = viewModel.MotorOilVModel.Viscosity!;
                    motorOil.Composition = viewModel.MotorOilVModel.Composition!;
                    motorOil.Volume = viewModel.MotorOilVModel.Volume;

                    motorOil.Save();
                    TempData["Success"] = "MotorOil edited successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
            catch (GenericNotFoundException<MotorOilController> ex)
            {
				_logger.LogError(ex, ex.Message);
				return RedirectToAction("Index");
            }
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            try
            {
                var motorOil = _motorOilService.GetMotorOilModel(id);
				_imageManager.RemoveFile(WebConstants.MotorOilsImages, motorOil!.PictureUrl!);
				motorOil.Delete();
                TempData["Success"] = "MotorOil deleted successfully";
                return RedirectToAction("Index");
            }

            catch (GenericNotFoundException<MotorOilController> ex)
            {
				_logger.LogError(ex, ex.Message);
				return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
            }
            #endregion
        }
    }
}