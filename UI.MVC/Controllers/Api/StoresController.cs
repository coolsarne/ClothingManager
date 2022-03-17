using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using ClothingManager.BL;
using ClothingManager.BL.Domain;
using ClothingManager.UI.MVC.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ClothingManager.UI.MVC.Controllers.Api{
    
    [ApiController]
    [Route("api/stores")]
    public class StoresController : ControllerBase{
        private readonly IManager _mgr;

        public StoresController(IManager mgr){
            _mgr = mgr;
        }

        [HttpGet]
        public IActionResult Get(){
            IEnumerable<ClothingManager.BL.Domain.Store> allStores = _mgr.GetAllStores();
            if (allStores is null || !allStores.Any()){
                return NoContent();
            }

            return Ok(allStores);
        }

        [HttpGet("{storeId:int}")]
        public IActionResult Get(int storeId){
            ClothingManager.BL.Domain.Store store = _mgr.GetStore(storeId);
            if (store is null) return NotFound();

            return Ok(store);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ClothingManager.BL.Domain.Store responseStore){
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Store newStore = _mgr.AddStore(responseStore.City, responseStore.Zipcode, responseStore.Name);
            return CreatedAtAction("Get",new{storeId=newStore.Id},newStore);
        }

        [HttpPut]
        public IActionResult Put([FromBody] StoreDTO dto){
            ValidationContext ctx = new ValidationContext(dto);
            if (!Validator.TryValidateObject(dto, ctx, null, true)){
                return BadRequest();
            }

            ClothingManager.BL.Domain.Store store = _mgr.ChangeStore(dto.City, dto.Zipcode, dto.Name, dto.Id);
            return NoContent();
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] StorePatchDTO dto) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            Store storeToUpdate = _mgr.GetStore(dto.Id);

            JsonPatchDocument<Store> patchEntity = new JsonPatchDocument<Store>();

            if (dto.City is null){
                patchEntity.Remove(s => s.City);
            } else if (dto.City != storeToUpdate.City){
                patchEntity.Replace(s => s.City, dto.City);
            }

            if (dto.Name is null) {
                patchEntity.Remove(s => s.Name);
            } else if (dto.Name != storeToUpdate.Name) {
                patchEntity.Replace(s => s.Name, dto.Name);
            }

            if (dto.Zipcode is null) {
                patchEntity.Remove(s => s.Zipcode);
            } else if (dto.Zipcode != storeToUpdate.Zipcode) {
                patchEntity.Replace(s => s.Zipcode, dto.Zipcode);
            }

            Store updatedStore = _mgr.ChangeStoreWithPatch(dto.Id, patchEntity);


            return Ok(updatedStore);
        }
    }
}