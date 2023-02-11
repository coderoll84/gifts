using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mvc.Data.Models;
using Mvc.DataAccess.Interfaces;
using Mvc.Models;
using Mvc.Utilities;
using NuGet.Protocol.Core.Types;
using System.Text.RegularExpressions;

namespace Mvc.Controllers
{
    public class UsuarioController : BaseController<UsuarioModel, Usuario>
    {
        public UsuarioController(IRepositoryAsync<Usuario> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public override async Task<IActionResult> Save(UsuarioModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var responseLogin = ValidarUsuario(model.Login);
            var responsePass = ValidarPassword(model.Password, model.ConfirmPassword);
            if (!responsePass.IsValid) responseLogin.Errors.AddRange(responsePass.Errors);

            if (!responsePass.IsValid || !responseLogin.IsValid) return Conflict(responseLogin.Errors);
            
            Usuario entity;
            try
            {
                entity = _mapper.Map<Usuario>(model);

                if (model.Id > 0)
                {
                    await _repository.Update(entity);
                }
                else
                {
                    await _repository.Insert(entity);
                }
            }
            catch (DbUpdateException e)
            when (e.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                return Conflict("Registro duplicado.");
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }

            return Json(entity);
        }

        private ActionResponse<Usuario> ValidarUsuario(string usuario)
        {
            var response = new ActionResponse<Usuario>();
            var hasCharUser = new Regex(@"[a-zA-Z]+");
            var hasMiniMaxUser = new Regex(@".{6,15}");
            var hasNumberAndCharUser = new Regex(@"^\w+$");
            if (!hasCharUser.IsMatch(usuario)) response.Errors.Add("El Usuario debe contener al menos una letra.");
            if (!hasMiniMaxUser.IsMatch(usuario)) response.Errors.Add("El Usuario debe tener una longitud de entre 6 y 12 caracteres.");
            if (!hasNumberAndCharUser.IsMatch(usuario)) response.Errors.Add("El Usuario solo puede contener letras y números.");
            response.IsValid = response.Errors.Count == 0;

            return response;
        }

        private ActionResponse<Usuario> ValidarPassword(string password, string confirmarPassword)
        {
            var response = new ActionResponse<Usuario>();
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasNumber = new Regex(@"[0-9]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(password)) response.Errors.Add("La contraseña debe contener al menos una letra minúscula.");
            if (!hasUpperChar.IsMatch(password)) response.Errors.Add("La contraseña debe contener al menos una letra mayúscula.");
            if (!hasMiniMaxChars.IsMatch(password)) response.Errors.Add("La contraseña debe tener una longitud de entre 8 y 15 caracteres.");
            if (!hasNumber.IsMatch(password)) response.Errors.Add("La contraseña debe contener al menos un carácter numérico.");
            if (!hasSymbols.IsMatch(password)) response.Errors.Add("La contraseña debe contener al menos un carácter especial.");
            if (password != confirmarPassword) response.Errors.Add("La confirmación de la contraseña es incorrecta");
            response.IsValid = response.Errors.Count == 0;

            return response;
        }
    }
}