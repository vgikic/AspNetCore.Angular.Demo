using Angular.Core.Models.BindingModel;
using Angular.Core.Models.Dto;
using Angular.Core.Services;
using AutoMapper;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Portal.Hubs;
using Portal.Identity;
using Portal.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular.Core.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsApiController : BaseApiController
    {
        private IHubContext<ItemHub> itemHub;

        public static List<ItemDto> items = new List<ItemDto>
            {
                new ItemDto
                {
                     Id =1,
                     Name = "Item_1",
                     Email = "item1@gmail.com",
                     From= DateTime.UtcNow,
                     To = DateTime.UtcNow.AddDays(1),
                     CategoryId = 1,
                     IsActive = false,
                     Parts = new List<int>{1,3 }
                },
                new ItemDto
                {
                     Id =2,
                     Name = "Item_2",
                     Email = "item2@gmail.com",
                     From = DateTime.UtcNow.AddDays(15),
                     To = DateTime.UtcNow.AddDays(25),
                     CategoryId = 2,
                     IsActive = true,
                     Parts = new List<int>{2,3 }
                },
                 new ItemDto
                {
                     Id =3,
                     Name = "Item_3",
                     Email = "item3@gmail.com",
                     From = DateTime.UtcNow.AddDays(-20),
                     To = DateTime.UtcNow.AddDays(-10),
                     CategoryId = 1,
                     IsActive = true,
                     Parts = new List<int>{4,5 }
                },
                new ItemDto
                {
                     Id =4,
                     Name = "Item_4",
                     Email = "item4@gmail.com",
                     From = DateTime.UtcNow.AddDays(-15),
                     To = DateTime.UtcNow.AddDays(-5),
                     CategoryId = 3,
                     IsActive = true,
                     Parts = new List<int>{1,3,4 }
                },
                new ItemDto
                {
                     Id =5,
                     Name = "Item_5",
                     Email = "item5@gmail.com",
                     From = DateTime.UtcNow.AddDays(20),
                     To = DateTime.UtcNow.AddDays(30),
                     CategoryId = 3,
                     IsActive = true,
                     Parts = new List<int>{1,2 }
                },
                new ItemDto
                {
                     Id =6,
                     Name = "Item_6",
                     Email = "item6@gmail.com",
                     From = DateTime.UtcNow.AddDays(15),
                     To = DateTime.UtcNow.AddDays(35),
                     CategoryId = 1,
                     IsActive = false,
                     Parts = new List<int>{ }
                },
                new ItemDto
                {
                     Id =7,
                     Name = "Item_7",
                     Email = "item7@gmail.com",
                     From = DateTime.UtcNow.AddDays(20),
                     To = DateTime.UtcNow.AddDays(25),
                     CategoryId = 2,
                     IsActive = true,
                     Parts = new List<int>{ 3,4,5 }
                },
                new ItemDto
                {
                     Id =8,
                     Name = "Item_8",
                     Email = "item8@gmail.com",
                     From = DateTime.UtcNow.AddDays(-15),
                     To = DateTime.UtcNow.AddDays(30),
                     CategoryId = 2,
                     IsActive = true,
                     Parts = new List<int>{ 1 }
                },
                new ItemDto
                {
                     Id =9,
                     Name = "Item_9",
                     Email = "item8@gmail.com",
                     From = DateTime.UtcNow.AddDays(40),
                     To = DateTime.UtcNow.AddDays(50),
                     CategoryId = 3,
                     IsActive = true,
                     Parts = new List<int>{ 4, 5 }
                },
                new ItemDto
                {
                     Id =10,
                     Name = "Item_10",
                     Email = "item10@gmail.com",
                     From = DateTime.UtcNow.AddDays(45),
                     To = DateTime.UtcNow.AddDays(60),
                     CategoryId = 3,
                     IsActive = true,
                     Parts = new List<int>{ 3, 4, 5 }
                },
                new ItemDto
                {
                     Id =11,
                     Name = "Item_11",
                     Email = "item11@gmail.com",
                     From = DateTime.UtcNow.AddDays(2),
                     To = DateTime.UtcNow.AddDays(5),
                     CategoryId = 2,
                     IsActive = true,
                     Parts = new List<int>{ 1,5 }
                },
                new ItemDto
                {
                     Id =12,
                     Name = "Item_12",
                     Email = "item12@gmail.com",
                     From = DateTime.UtcNow.AddDays(2),
                     To = DateTime.UtcNow.AddDays(4),
                     CategoryId = 1,
                     IsActive = true,
                     Parts = new List<int>{ 1,2,3,4,5 }
                },

            };
        private static List<PartDto> parts = new List<PartDto>
            {
                new PartDto
                {
                    Id = 1,
                    Name = "Part_1"
                },
                new PartDto
                {
                    Id = 2,
                    Name = "Part_2"
                },
                new PartDto
                {
                    Id = 3,
                    Name = "Part_3"
                },
                new PartDto
                {
                    Id = 4,
                    Name = "Part_4"
                },
                new PartDto
                {
                    Id = 5,
                    Name = "Part_5"
                },
            };
        private static List<CategoryDto> categories = new List<CategoryDto>
            {
                new CategoryDto
                {
                    Id = 1,
                    Name= "Category_1"
                },
                new CategoryDto
                {
                    Id = 2,
                    Name= "Category_2"
                },
                new CategoryDto
                {
                    Id = 3,
                    Name= "Category_3"
                },
            };
        public ItemsApiController(
            IHubContext<ItemHub> itemHub,
            ILogger<ItemsApiController> logger,
            IMapper mapper
            ) : base(logger, mapper)
        {
            this.itemHub = itemHub;
        }


        [HttpGet("overview")]
        public IActionResult GetItemsOverview(DataSourceLoadOptions options)
        {
            try
            {
                var result = DataSourceLoader.Load(items, options);
                return Ok(result);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        [HttpGet("store")]
        public IActionResult GetItems()
        {
            try
            {
                return Ok(items.Take(3));
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            try
            {
                items = items.Where(i => i.Id != id).ToList();
                return NoContent();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetItem(int id)
        {
            try
            {
                var item = items.FirstOrDefault(i => i.Id == id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        [HttpGet("categories/lookup")]
        public IActionResult GetCategoriesForLookup()
        {
            try
            {
                return Ok(categories);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        [HttpGet("parts/lookup")]
        public IActionResult GetPartsForLookup()
        {
            try
            {
                return Ok(parts);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] JsonPatchDocument<ItemBindingModel> model)
        {
            try
            {
                if (model == null)
                    return BadRequest(ModelState);

                // Simulates fetching item from DB
                var itemFromDb = items.FirstOrDefault(i => i.Id == id);
                var itemBm = base.mapper.Map<ItemBindingModel>(itemFromDb);

                model.ApplyTo(itemBm, ModelState);

                if (!TryValidateModel(itemBm))
                    return BadRequest(ModelState);

                base.mapper.Map(itemBm, itemFromDb);

                model.ApplyTo(itemBm, ModelState);

                // DB Save would go here...


                await itemHub.Clients.All.SendAsync("ItemUpdate", itemFromDb);

                return NoContent();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        [HttpPost("")]
        public IActionResult CreateItem([FromBody] ItemBindingModel model)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                    return BadRequest(ModelState);

                var item = base.mapper.Map<ItemDto>(model);
                item.Id = items.Max(i => i.Id) + 1;
                // Simulates saving item to DB
                items.Add(item);

                return Ok(item);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        [HttpGet("validation/email")]
        public IActionResult IsEmailUnique(string email, int id)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError("Email", "Null or empty email");
                    return BadRequest(ModelState);
                }
                return Ok(items.Any(i => i.Id != id && i.Email.ToLowerInvariant() == email.ToLowerInvariant()));
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        [HttpGet("error")]
        public IActionResult GetBadRequest()
        {
            try
            {
                ModelState.AddModelError("ERROR", "SHIT HAPPENED, SRY!");
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }


        [Authorize]
        [HttpGet("authorized")]
        public IActionResult GetDataForAuthorizedUsers()
        {
            try
            {
                return Ok(new ResponseDto { Message = "TOP SECRET STRING FOR AUTHORIZED USERS" });
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }


        [Authorize(Policy = RoleClaims.Admin)]
        [HttpGet("authorized/admin")]
        public IActionResult GetDataForAdmins()
        {
            try
            {
                return Ok(new ResponseDto { Message = "TOP SECRET STRING FOR ADMINS" });
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        [Authorize(Policy = RoleClaims.Support)]
        [HttpGet("authorized/support")]
        public IActionResult GetDataForSupport()
        {
            try
            {
                return Ok(new ResponseDto { Message = "TOP SECRET STRING FOR SUPPORT" });
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        [Authorize(Policy = RoleClaims.God)]
        [HttpGet("authorized/god")]
        public IActionResult TryGettingUnattainableData()
        {
            try
            {
                return Ok(new ResponseDto { Message = "YOU ARE NOT SUPPOSED TO GET THIS!" });
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }
    }
}