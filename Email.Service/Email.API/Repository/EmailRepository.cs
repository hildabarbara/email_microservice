using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Email.API
{
    public class EmailRepository : IEmailRepository
    {
        protected readonly EmailContext context;
        protected readonly DbSet<EmailModel> dbSet;
        private readonly IDistributedCache _cache;
        private const string KEY_EMAILS = "EMAILS";

        public EmailRepository(EmailContext context, IDistributedCache cache)
        {
            this.context = context;
            dbSet = context.Set<EmailModel>();
            _cache = cache;
        }

        public async Task<IEnumerable<EmailModel>> ListEmailsAsync()
        {
            try
            {
                return await CacheRefresh();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<EmailModel> SaveEmailAsync(string email)
        {
            EntityEntry<EmailModel> entityEntry;

            try
            {
                entityEntry = await dbSet.AddAsync(new EmailModel() { Email = email});
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            await CacheRefresh();
            return entityEntry.Entity;
        }

        private async Task<IEnumerable<EmailModel>> CacheRefresh()
        {
            var dataCache = await _cache.GetStringAsync(KEY_EMAILS);

            if (string.IsNullOrWhiteSpace(dataCache))
            {
                var cacheSettings = new DistributedCacheEntryOptions();
                cacheSettings.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                var emailsDb = dbSet.ToListAsync();

                var emailsJson = JsonConvert.SerializeObject(emailsDb);

                await _cache.SetStringAsync(KEY_EMAILS, emailsJson, cacheSettings);

                return await emailsDb;
            }

            var emailsCache = JsonConvert.DeserializeObject<IEnumerable<EmailModel>>(dataCache);

            return await Task.FromResult(emailsCache);
        }
    }
}
