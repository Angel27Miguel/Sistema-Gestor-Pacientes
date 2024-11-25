using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Services;
using SistemaGestorPacientesApp.Core.Application.ViewModels.User;
using SistemaGestorPacientesApp.Core.Domain.Entities;

namespace SistemaGestorPacientesApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConsultorioRepository _consultorioRepository;

        public UserService(IUserRepository userRepository, IConsultorioRepository consultorioRepository)
        {
           _consultorioRepository = consultorioRepository;
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> Login(LoginViewModel vm)
        {

            Usuario user = await _userRepository.LoginAsync(vm);

            if (user == null)
            {
                return null;
            }

            return new UserViewModel
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Correo = user.Correo,
                NombreUsuario = user.NombreUsuario,
                EsAdministrador = user.EsAdministrador
            };
        }


        public async Task Update(SaveUserViewModel vm)
        {
            Usuario user = await _userRepository.GetByIdAsync(vm.Id);

           
            user.Nombre = vm.Nombre;
            user.Apellido = vm.Apellido;
            user.NombreUsuario = vm.NombreUsuario;
            user.Correo = vm.Correo;
            user.EsAdministrador = vm.EsAdministrador;

            // Solo actualizar la contraseña si se coloca una nueva
            if (!string.IsNullOrEmpty(vm.Contraseña))
            {
                user.Contraseña = vm.Contraseña;
            }

            await _userRepository.UpdateAsync(user);
        }

        public async Task<SaveUserViewModel> Add(SaveUserViewModel vm)
        {

            if (await _userRepository.UserNameExistsAsync(vm.NombreUsuario))
            {
                throw new Exception("El nombre de usuario ya existe. Por favor, elige otro.");
            }

            Consultorio consultorio = new()
            {
                Nombre = vm.Nombre + " " + vm.Apellido + " Consultorio"
            };

            consultorio = await _consultorioRepository.AddAsync(consultorio);

            Usuario user = new()
            {
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                NombreUsuario = vm.NombreUsuario,
                Contraseña = vm.Contraseña,
                Correo = vm.Correo,
                ConsultorioId = consultorio.Id,
                EsAdministrador = vm.EsAdministrador
            };

            user = await _userRepository.AddAsync(user);

            SaveUserViewModel userVm = new()
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                NombreUsuario = user.NombreUsuario,
                Contraseña = user.Contraseña,
                Correo = user.Correo,
                ConsultorioId = user.ConsultorioId,
                EsAdministrador = user.EsAdministrador
            };

            return userVm;
        }



        public async Task Delete(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            await _userRepository.DeleteAsync(user);
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            SaveUserViewModel vm = new()
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                NombreUsuario = user.NombreUsuario,
                Contraseña = user.Contraseña,
                Correo = user.Correo,
                ConsultorioId = user.ConsultorioId,
                EsAdministrador= user.EsAdministrador
            };

            return vm;
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var userList = await _userRepository.GetAllWithIncludeAsync(new List<string> { "Consultorio" });

            return userList.Select(user => new UserViewModel
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                NombreUsuario = user.NombreUsuario,
                Correo = user.Correo,
                ConsultorioId = user.ConsultorioId,
                EsAdministrador = user.EsAdministrador
                
            }).ToList();
        }


    }
}
