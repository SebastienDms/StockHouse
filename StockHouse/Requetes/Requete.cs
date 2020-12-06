using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using StockHouse.Models;

namespace StockHouse.Requetes
{
    public class Requete<T> : IRequete
	/* Le type T doit être une classe, implémenter l'interface IModel et doit avoir un constructeur public */
        where T : class, IModel, new() 
    {
		protected readonly DbSet<T> Table;
		protected readonly BdStockHouse Bdd;

        public Requete()
        {
			Bdd = new BdStockHouse();
            Table = Bdd.Set<T>();
        }

        public async Task<int> Save()
        {
            int id = await Bdd.SaveChangesAsync();

            return id;
        }
        public void Dispose()
		{
			Bdd.Dispose();
		}

		/**
		 * <summary>Récupère un enregistrement via son id</summary>
		 * <param name="id">Id de l'élément à récupérer</param>
		 * <returns>Retourne l'enregistrement trouvé</returns>
		 */
		public virtual T GetById(int id)
		{
            return Table.Find(id);
		}

		/**
		 * <summary>Récupère un enregistrement via son id</summary>
		 * <param name="id">Id de l'élément à récupérer</param>
		 * <returns>Retourne l'enregistrement trouvé</returns>
		 */
		public virtual async Task<T> GetByIdAsync(int id)
		{
			return await Table.FindAsync(id);
		}

		/**
		 * <summary>Récupère tous les enregistrements de la table</summary>
		 * <returns>Retourne une liste des modèles</returns>
		 */
		public virtual IList<T> GetAll()
		{
			return Table.ToList();
		}

		/**
		 * <summary>Récupère tous les enregistrements de la table</summary>
		 * <returns>Retourne une liste des modèles</returns>
		 */
		public virtual async Task<IList<T>> GetAllAsync()
		{
			return await Table.ToListAsync();
		}

		/**
		 * <summary>Ajoute un enregistrement</summary>
		 * <param name="model">Modèle à sauvegarder</param>
		 */
		public virtual int Add(T model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			Table.Add(model);
			Bdd.SaveChanges();

			return model.Id;
		}

		/**
		 * <summary>Ajoute un enregistrement</summary>
		 * <param name="model">Modèle à sauvegarder</param>
		 */
		public virtual async Task<int> AddAsync(T model)
		{
			return await Task.Run(() => Add(model));
		}

		/**
		 * <summary>Supprime un enregistrement via son id</summary>
		 * <param name="id">Id de l'élément à supprimer</param>
		 * <returns>Retourne l'enregistrement supprimé</returns>
		 */
		public virtual T Delete(int id)
		{
			T model = new T()
			{
				Id = id
			};

			Table.Attach(model);
			T result = Table.Remove(model);
			Bdd.SaveChanges();

			return result;
		}

		/**
		 * <summary>Supprime un enregistrement via son id</summary>
		 * <param name="id">Id de l'élément à supprimer</param>
		 * <returns>Retourne l'enregistrement supprimé</returns>
		 */
		public virtual async Task<T> DeleteAsync(int id)
		{
			return await Task.Run(() => Delete(id));
		}

        /*********************************************************/
        /* UPDATE */
        public void UpdatePiece(Piece modifPiece)
        {
            Piece updatePiece = (from p in Bdd.Pieces
                where p.Id == modifPiece.Id
                select p).SingleOrDefault();

            updatePiece.Nom = modifPiece.Nom;

            Bdd.SaveChanges();
        }
	}
}