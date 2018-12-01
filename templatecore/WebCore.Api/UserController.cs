//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Threading.Tasks;
//using WebCore.Services.Share.Admins.Users;
//using WebCore.Services.Share.Admins.Users.Dto;
//using WebCore.Services.Share.Languages;
//using WebCore.Utils.Config;
//using WebCore.Utils.ModelHelper;

//namespace WebCore.Api
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : BaseApiController
//    {
//        private readonly IUserService userService;
//        private readonly ILanguageProviderService languageService;

//        public UserController(IServiceProvider serviceProvider, IUserService userService, ILanguageProviderService languageService)
//            : base(serviceProvider)
//        {
//            this.userService = userService;
//            this.languageService = languageService;
//        }

//        [HttpPost("Add")]
//        public async Task<IActionResult> Add(UserInfoInput addInput)
//        {
//            if (!ModelState.IsValid)
//            {
//                string errorLangCode = ConstantConfig.WebApiResultMessage.Error;
//                string errorMessage = languageService.GetlangByKey(errorLangCode);
//                return BadRequest(new
//                {
//                    Message = errorMessage,
//                    ModelErrors = GetModelErrors()
//                });
//            }

//            bool result = await userService.Add(addInput);

//            if (!result)
//            {
//                string errorLangCode = ConstantConfig.WebApiResultMessage.Error;
//                string errorMessage = languageService.GetlangByKey(errorLangCode);
//                return BadRequest(errorMessage);
//            }

//            return Ok(addInput);
//        }

//        [HttpPost("Active")]
//        public async Task<IActionResult> Active(UpdateTokenModel<string> entityId)
//        {
//            // check modelstate
//            if (!ModelState.IsValid)
//            {
//                string errorLangCode = ConstantConfig.WebApiResultMessage.Error;
//                string errorMessage = languageService.GetlangByKey(errorLangCode);
//                return BadRequest(new
//                {
//                    StatusCode = ConstantConfig.WebApiStatusCode.ModelInValid,
//                    Message = errorMessage,
//                    ModelErrors = GetModelErrors()
//                });
//            }

//            //find last modified
//            Entities.WebCoreUser lastEntity = userService.GetById(entityId);

//            if (lastEntity == null)
//            {
//                return NotFound();
//            }

//            // check update token
//            if (!lastEntity.UpdateToken.GetValueOrDefault().Equals(entityId.UpdateToken))
//            {
//                string errorLangCode = ConstantConfig.WebApiResultMessage.UpdateTokenNotMatch;
//                string errorMessage = languageService.GetlangByKey(errorLangCode);
//                return BadRequest(new
//                {
//                    StatusCode = ConstantConfig.WebApiStatusCode.Warning,
//                    Message = errorMessage
//                });
//            }

//            bool result = await userService.SetActive(entityId, ConstantConfig.UserRecordStatus.Active);

//            // if has any error
//            if (!result)
//            {
//                string errorLangCode = ConstantConfig.WebApiResultMessage.Error;
//                string errorMessage = languageService.GetlangByKey(errorLangCode);
//                return BadRequest(new
//                {
//                    StatusCode = ConstantConfig.WebApiStatusCode.Error,
//                    Message = errorMessage
//                });
//            }

//            return Ok();
//        }

//        [HttpPut("UpdateInfo")]
//        public async Task<IActionResult> UpdateInfo(UserInfoInput updateInput)
//        {
//            if (!ModelState.IsValid)
//            {
//                string errorLangCode = ConstantConfig.WebApiResultMessage.Error;
//                string errorMessage = languageService.GetlangByKey(errorLangCode);
//                return BadRequest(new
//                {
//                    Message = errorMessage,
//                    ModelErrors = GetModelErrors()
//                });
//            }

//            Entities.WebCoreUser lastEntity = userService.GetById(updateInput);

//            if (lastEntity == null)
//            {
//                return NotFound();
//            }

//            // check update token
//            if (!lastEntity.UpdateToken.GetValueOrDefault().Equals(updateInput.UpdateToken))
//            {
//                string errorLangCode = ConstantConfig.WebApiResultMessage.UpdateTokenNotMatch;
//                string errorMessage = languageService.GetlangByKey(errorLangCode);
//                return BadRequest(new
//                {
//                    StatusCode = ConstantConfig.WebApiStatusCode.Warning,
//                    Message = errorMessage
//                });
//            }

//            bool result = await userService.UpdateInfo(updateInput);

//            if (!result)
//            {
//                string errorLangCode = ConstantConfig.WebApiResultMessage.Error;
//                string errorMessage = languageService.GetlangByKey(errorLangCode);
//                return BadRequest(errorMessage);
//            }

//            return Ok(updateInput);
//        }

//        [HttpGet("GetGet")]
//        public IActionResult GetGet()
//        {
//            return Ok("ok");
//        }


//        [HttpDelete("DeleteUser")]
//        public async Task<IActionResult> DeleteUser(UpdateTokenModel<string> entityId)
//        {
//            // check modelstate
//            if (!ModelState.IsValid)
//            {
//                string errorLangCode = ConstantConfig.WebApiResultMessage.Error;
//                string errorMessage = languageService.GetlangByKey(errorLangCode);
//                return BadRequest(new
//                {
//                    StatusCode = ConstantConfig.WebApiStatusCode.ModelInValid,
//                    Message = errorMessage,
//                    ModelErrors = GetModelErrors()
//                });
//            }

//            Entities.WebCoreUser lastEntity = userService.GetById(entityId);

//            if (lastEntity == null)
//            {
//                return NotFound();
//            }

//            // check update token
//            if (!lastEntity.UpdateToken.GetValueOrDefault().Equals(entityId.UpdateToken))
//            {
//                string errorLangCode = ConstantConfig.WebApiResultMessage.UpdateTokenNotMatch;
//                string errorMessage = languageService.GetlangByKey(errorLangCode);
//                return BadRequest(new
//                {
//                    StatusCode = ConstantConfig.WebApiStatusCode.Warning,
//                    Message = errorMessage
//                });
//            }

//            bool result = await userService.Delete(entityId);

//            // if has any error
//            if (!result)
//            {
//                string errorLangCode = ConstantConfig.WebApiResultMessage.Error;
//                string errorMessage = languageService.GetlangByKey(errorLangCode);
//                return BadRequest(new
//                {
//                    StatusCode = ConstantConfig.WebApiStatusCode.Error,
//                    Message = errorMessage
//                });
//            }

//            return Ok();
//        }

//        [HttpDelete("InActive")]
//        public async Task<IActionResult> InActive(UpdateTokenModel<string> entityId)
//        {
//            // check modelstate
//            if (!ModelState.IsValid)
//            {
//                string errorLangCode = ConstantConfig.WebApiResultMessage.Error;
//                string errorMessage = languageService.GetlangByKey(errorLangCode);
//                return BadRequest(new
//                {
//                    StatusCode = ConstantConfig.WebApiStatusCode.ModelInValid,
//                    Message = errorMessage,
//                    ModelErrors = GetModelErrors()
//                });
//            }

//            Entities.WebCoreUser lastEntity = userService.GetById(entityId);

//            if (lastEntity == null)
//            {
//                return NotFound();
//            }

//            // check update token
//            if (!lastEntity.UpdateToken.GetValueOrDefault().Equals(entityId.UpdateToken))
//            {
//                string errorLangCode = ConstantConfig.WebApiResultMessage.UpdateTokenNotMatch;
//                string errorMessage = languageService.GetlangByKey(errorLangCode);
//                return BadRequest(new
//                {
//                    StatusCode = ConstantConfig.WebApiStatusCode.Warning,
//                    Message = errorMessage
//                });
//            }

//            bool result = await userService.SetActive(entityId, ConstantConfig.UserRecordStatus.InActive);

//            // if has any error
//            if (!result)
//            {
//                string errorLangCode = ConstantConfig.WebApiResultMessage.Error;
//                string errorMessage = languageService.GetlangByKey(errorLangCode);
//                return BadRequest(new
//                {
//                    StatusCode = ConstantConfig.WebApiStatusCode.Error,
//                    Message = errorMessage
//                });
//            }

//            return Ok();
//        }
        
//        [HttpGet("GetAll")]
//        public IActionResult GetAll(UserFilterInput filterInput = null)
//        {
//            if (filterInput == null)
//            {
//                filterInput = new UserFilterInput();
//            }

//            PagingResultDto<UserDto> result = userService.GetAllByPaging(filterInput);

//            return Ok(result);
//        }

//        [HttpGet("GetInputById")]
//        public IActionResult GetInputById(EntityId<string> entityId)
//        {
//            UserInfoInput result = userService.GetInputById(entityId);
//            if (result == null)
//            {
//                return NotFound();
//            }
//            return Ok(result);
//        }
//    }
//}
