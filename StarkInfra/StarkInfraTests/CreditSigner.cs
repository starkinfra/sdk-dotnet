using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class CreditSignerTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void ResendToken()
        {
            List<CreditNote> notes = CreditNote.Create(new List<CreditNote>() { CreditNoteTest.Example() });
            CreditNote note = notes.First();
            Assert.NotNull(note.ID);
            Assert.NotEmpty(note.Signers);

            CreditSigner signer = note.Signers.First();
            CreditSigner resentSigner = CreditSigner.ResendToken(signer.ID);
            Assert.Equal(signer.ID, resentSigner.ID);
        }

        [Fact]
        public void ResendTokenFail()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => CreditSigner.ResendToken("000"));
        }
    }
}
