using System;
using StarkInfra;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class PixDirectorTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Create()
        {
            PixDirector director = PixDirector.Create(Example());
            Assert.NotNull(director.Status);
            TestUtils.Log(director);
        }

        internal static PixDirector Example()
        {
            return new PixDirector(
                email: "bacen@starkbank.com",
                name: "Jon Snow",
                password: "12345678",
                phone: "+551141164616",
                taxID: "123.456.789-0",
                teamEmail: "bacen@starkbank.com",
                teamPhones: new List<string>(){
                    "+551141164616"
                }
            );
        }
    }
}
