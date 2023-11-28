using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using StockHouse.Models;
using StockHouse.Models.tables;

namespace StockHouse.Requetes
{
  public class Requete<T> : IRequete
      /* Le type T doit être une classe, implémenter l'interface IModelBase et doit avoir un constructeur public */
      where T : class, IModelBase, new()
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
     * <summary>Récupère un enregistrement via son id</summary>
     * <param name="id">Id de l'élément à récupérer</param>
     * <returns>Retourne l'enregistrement trouvé</returns>
     */
    public virtual async Task<User> GetUserByMailAsync(string mail)
    {
      var user = await Bdd.Users.Where(u => u.AdresseMail == mail).FirstOrDefaultAsync();
      return user;
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
    public virtual bool Add(T model)
    {
      if (model == null)
        throw new ArgumentNullException(nameof(model));

      Table.Add(model);
      try
      {
        var res = Bdd.SaveChanges();
      }
      catch (DbUpdateException e)
      {
        Console.WriteLine(e);

        //var test = ((SqlException) e.InnerException.InnerException).Number;
        if (((SqlException)e.InnerException.InnerException).Number == 2601)
        /* Capte l'exception qui signale la violation d'unicité sur l'e-mail */
        {
          return false;
        }

        throw;
      }

      return true;
    }

    /**
		 * <summary>Ajoute un enregistrement</summary>
		 * <param name="model">Modèle à sauvegarder</param>
		 */
    public virtual async Task<bool> AddAsync(T model)
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
    public virtual async Task<int> UpdateAsync(T newModel)
    {
      var updateElement = await GetByIdAsync(newModel.Id);

      Bdd.Entry(updateElement).CurrentValues.SetValues(newModel);

      return await Bdd.SaveChangesAsync();
    }
    #region update
    //public async Task<int> UpdatePiece(Piece modifPiece)
    //{
    //    var updatePiece = await (from p in Bdd.Pieces
    //        where p.Id == modifPiece.Id
    //        select p).FirstOrDefaultAsync();

    //    updatePiece.Nom = modifPiece.Nom;

    //    return await Bdd.SaveChangesAsync();
    //}
    //public async Task<int> UpdateAchat(Achat modifAchat)
    //{
    //    var updateAchat = await (from p in Bdd.Achats
    //        where p.Id == modifAchat.Id
    //        select p).FirstOrDefaultAsync();

    //    updateAchat.Prix = modifAchat.Prix;

    //    return await Bdd.SaveChangesAsync();
    //}
    //public async Task<int> UpdateMagasin(Magasin modifMagasin)
    //{
    //    var updateMagasin = await (from p in Bdd.Magasins
    //        where p.Id == modifMagasin.Id
    //        select p).FirstOrDefaultAsync();

    //    updateMagasin.Nom = modifMagasin.Nom;

    //    return await Bdd.SaveChangesAsync();
    //}
    //public async Task<int> UpdateProduit(Produit modifProduit)
    //{
    //    var updateProduit = await (from p in Bdd.Produits
    //        where p.Id == modifProduit.Id
    //        select p).FirstOrDefaultAsync();

    //    updateProduit.Nom = modifProduit.Nom;

    //    return await Bdd.SaveChangesAsync();
    //}
    //public async Task<int> UpdateUser(User modifUser)
    //{
    //    var updateProduit = await (from p in Bdd.Users
    //        where p.Id == modifUser.Id
    //        select p).FirstOrDefaultAsync();

    //    updateProduit.Nom = modifUser.Nom;

    //    return await Bdd.SaveChangesAsync();
    //}
    #endregion
    /*********************************************************/
    /* EXIST */
    public async Task<bool> VerifLogin(User logUser)
    {
      logUser.MotDePasse = Cryptage.EncryptStringToBytes_Aes(logUser.MotDePasse);

      return await Bdd.Users.AnyAsync(u =>
          u.AdresseMail == logUser.AdresseMail && u.MotDePasse == logUser.MotDePasse);
    }
    public async Task<bool> MailExist(string email)
    {
      return await Bdd.Users.AnyAsync(user => string.Compare(user.AdresseMail, email) == 0);
    }
    /* test nom existe générique -> IModelNom pour utiliser le champ Nom des tables */
    public async Task<bool> NameExist<TNom>(TNom testElement)
        /* Précise IModelNom pour accéder à la propriété Nom  */
        where TNom : class, IModelNom, new()
    {
      DbSet<TNom> tableNom = Bdd.Set<TNom>();

      return await tableNom.AnyAsync(table => string.Compare(table.Nom, testElement.Nom, StringComparison.CurrentCultureIgnoreCase) == 0);
    }
  }
}