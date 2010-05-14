
using System;
using System.Collections.Generic;

namespace VolunteerTracker
{


	public class CachedActiveRecord : ActiveRecord
	{
		private static Dictionary<string, ActiveRecord> cache = new Dictionary<string, ActiveRecord>();		

		public static void ClearCache()
        {
            cache.Clear();
        }
		
		public static bool InCache(ActiveRecord record)
        {
			return cache.ContainsKey(GetCacheKey(record));
        }

		
		public static bool RemoveFromCach(ActiveRecord record)
		{
			if (cache.ContainsKey(GetCacheKey(record)))
			{
				cache.Remove(GetCacheKey(record));
				return true;
			}
			return false;
		}              
		
		private static string GetCacheKey(ActiveRecord record)
		{
			return record.GetType().Name + " " + record.Id;
		}
		
		public static void AddToCache(ActiveRecord record)
		{
			cache[GetCacheKey(record)] = record;
		}
		
		public CachedActiveRecord () : base()
		{
		}		
		
		public CachedActiveRecord(long id)
		{			
			this.Id = id;
			if (!InCache(this))
            {				
            	if (!Database.RecordExists(this))
            	{
                	SetDefaultACL();
            	}
            	PopulateValues();            					
                AddToCache(this);
            } else {
				SetEqual(cache[GetCacheKey(this)]);
			}
		}
		
		public override bool Save()
        {
            bool retVal = base.Save();
            AddToCache(this);
            return retVal;
        }

        public override int Delete()
        {
            int retVal = base.Delete();
            RemoveFromCach(this);
            return retVal;
        }
		
		
	}
}
