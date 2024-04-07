using System;
using Microsoft.EntityFrameworkCore;
using petConnection.Backend.Data;
using petConnection.Backend.Repositories.Interfaces;
using petConnection.FrontEnd.Shared.Responses;

namespace petConnection.Backend.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRespository<T> where T : class
    {

        private readonly DbSet<T> _entity;
        private readonly DataContext _context; // underline for private attributes

        public GenericRepository(DataContext context)
        {
            _context = context; // represent all database
            _entity = _context.Set<T>(); // represent entity to modify
        }


        // virtual significa que son metodos que se pueden sobreescribir
        public virtual async Task<ActionResponse<T>> AddAsync(T entity)
        {
            _context.Add(entity);

            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<T> { WasSuccess = true, Result = entity };
            }
            catch (DbUpdateException)
            {
                return DbUpdateExceptionactionResponse();
            }
            catch (Exception exception)
            {
                return ExceptionactionResponse(exception);
            }

        }


        public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
        {
            var row = await _entity.FindAsync(id);

            if (row is null)
            {
                return new ActionResponse<T>
                {
                    WasSuccess = false,
                    Message = "Registro no encontrado"
                };
            }

            try
            {
                _entity.Remove(row);
                await _context.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    WasSuccess = true,
                };
            }
            catch
            {
                return new ActionResponse<T>
                {
                    WasSuccess = false,
                    Message = "No se puede borrar por que tiene registros relacionados"
                };
            }
        }

        public virtual async Task<ActionResponse<T>> GetAsync(int id)
        {
            var row = await _entity.FindAsync(id);

            if (row is null)
            {
                return new ActionResponse<T>
                {
                    WasSuccess = false,
                    Message = "Registro no encontrado"
                };
            }

            return new ActionResponse<T>
            {
                WasSuccess = true,
                Result = row,
            };

        }

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync()
        {
            return new ActionResponse<IEnumerable<T>>
            {
                WasSuccess = true,
                Result = await _entity.ToListAsync()
            };
        }

        public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
        {
            _context.Update(entity);

            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<T> { WasSuccess = true, Result = entity };
            }
            catch (DbUpdateException)
            {
                return DbUpdateExceptionactionResponse();
            }
            catch (Exception exception)
            {
                return ExceptionactionResponse(exception);
            }
        }


        private ActionResponse<T> ExceptionactionResponse(Exception exception)
        {
            return new ActionResponse<T>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }

        private ActionResponse<T> DbUpdateExceptionactionResponse()
        {
            return new ActionResponse<T>
            {
                WasSuccess = false,
                Message = "Ya existe el registro que quieres crear"
            };
        }
    }
}

