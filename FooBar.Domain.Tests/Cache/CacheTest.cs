using Domain.DistributedCache;
using FooBar.Domain.Entities;
using Infrastructure.DistributedCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace FooBar.Domain.Tests.Person
{
    [TestClass]
    public class CacheTest
    {

        IApplicationCache<CommonLanguage> _memoryCache = default!;

        [TestInitialize]
        public void Init()
        {
            _memoryCache = new MemoryCache<CommonLanguage>();
        }

        [TestMethod]
        public async Task SuccessToRegisterPerson()
        {
            var myId = Guid.NewGuid();
            CommonLanguage language = new CommonLanguage(myId, "C#", "This a great language.", DateTime.Now.AddYears(-20));
            await _memoryCache.SetValueAsync(myId, language);
            var mySavedLanguage = await _memoryCache.GetValueAsync(myId);
            Assert.IsInstanceOfType(mySavedLanguage, typeof(CommonLanguage));
        }
    }
}
