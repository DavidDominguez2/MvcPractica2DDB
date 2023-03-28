using Microsoft.EntityFrameworkCore;
using MvcPractica2DDB.Data;
using MvcPractica2DDB.Models;

namespace MvcPractica2DDB.Repositories {
    public class RepositoryLibros {

        private LibrosContext context;

        public RepositoryLibros(LibrosContext context) {
            this.context = context;
        }

        public async Task<List<Genero>> GetGenerosAsync() {
            return await this.context.Generos.ToListAsync();
        }

        private async Task<List<Libro>> GetAllLibrosAsync() {
            return await this.context.Libros.ToListAsync();
        }

        public async Task<List<Libro>> GetLibrosGeneroAsync(int idgenero) {
            return await this.context.Libros.Where(z => z.IdGenero == idgenero).ToListAsync();
        }

        public async Task<Libro> FindLibroAsync(int idlibro) {
            return await this.context.Libros.FirstOrDefaultAsync(x => x.IdLibro == idlibro);
        }

        public async Task<LibrosPaginados> GetLibrosPaginadosAsync(int posicion) {
            List<Libro> libros = await this.GetAllLibrosAsync();
            int numregistros = libros.Count;

            List<Libro> listalibrospag = libros.Skip(posicion).Take(10).ToList();

            LibrosPaginados librosPaginados = new LibrosPaginados {
                Libros = listalibrospag,
                NumRegistros = numregistros
            };

            return librosPaginados;
        }

        public async Task<Usuario> LoginUserAsync(string email, string password) {
            Usuario user = await this.context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) {
                return null;
            } else {
                if (user.Pass == password) {
                    return user;
                } else {
                    return null;
                }
            }
        }
    }
}
