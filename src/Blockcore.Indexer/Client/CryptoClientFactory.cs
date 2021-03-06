namespace Blockcore.Indexer.Client
{
   using Microsoft.Extensions.Caching.Memory;
   #region Using Directives

   // using System.Runtime.Caching;

   #endregion

   /// <summary>
   ///  a factory to create clients.
   /// </summary>
   public class CryptoClientFactory
   {
      /// <summary>
      ///     Defines a cache object to hold storage sources.
      /// </summary>
      private static readonly MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());

      /// <summary>
      /// A static method to create a client.
      /// </summary>
      public static BitcoinClient Create(string connection, int port, string user, string encPass, bool secure)
      {
         // Put a lock on the cache to avoid creating random number of clients on startup.
         lock (Cache)
         {
            // Set cache key name
            string cacheKey = string.Format("{0}:{1}:{2}:{3}", connection, port, user, secure);
            return Cache.GetOrCreate(cacheKey, t => BitcoinClient.Create(connection, port, user, encPass, secure));
         }
      }
   }
}
