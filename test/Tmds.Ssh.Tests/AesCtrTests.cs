using Xunit;

namespace Tmds.Ssh.Tests;

public class AesCtrTests
{
    // https://datatracker.ietf.org/doc/html/rfc3686.html#section-6
    [Theory]
    [InlineData(
        "AE6852F8121067CC4BF7A5765577F39E",
        "0000000000000000",
        "00000030",
        "53696E676C6520626C6F636B206D7367",
        "E4095D4FB7A7B3792D6175A3261311B8")]
    [InlineData(
        "7E24067817FAE0D743D6CE1F32539163",
        "C0543B59DA48D90B",
        "006CB6DB",
        "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F",
        "5104A106168A72D9790D41EE8EDAD388EB2E1EFC46DA57C8FCE630DF9141BE28")]
    [InlineData(
        "7691BE035E5020A8AC6E618529F9A0DC",
        "27777F3F4A1786F0",
        "00E0017B",
        "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20212223",
        "C1CF48A89F2FFDD9CF4652E9EFDB72D74540A42BDE6D7836D59A5CEAAEF3105325B2072F")]
    [InlineData(
        "16AF5B145FC9F579C175F93E3BFB0EED863D06CCFDB78515",
        "36733C147D6D93CB",
        "00000048",
        "53696E676C6520626C6F636B206D7367",
        "4B55384FE259C9C84E7935A003CBE928")]
    [InlineData(
        "7C5CB2401B3DC33C19E7340819E0F69C678C3DB8E6F6A91A",
        "020C6EADC2CB500D",
        "0096B03B",
        "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F",
        "453243FC609B23327EDFAAFA7131CD9F8490701C5AD4A79CFC1FE0FF42F4FB00")]
    [InlineData(
        "02BF391EE8ECB159B959617B0965279BF59B60A786D3E0FE",
        "5CBD60278DCC0912",
        "0007BDFD",
        "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20212223",
        "96893FC55E5C722F540B7DD1DDF7E758D288BC95C69165884536C811662F2188ABEE0935")]
    [InlineData(
        "776BEFF2851DB06F4C8A0542C8696F6C6A81AF1EEC96B4D37FC1D689E6C1C104",
        "DB5672C97AA8F0B2",
        "00000060",
        "53696E676C6520626C6F636B206D7367",
        "145AD01DBF824EC7560863DC71E3E0C0")]
    [InlineData(
        "F6D66D6BD52D59BB0796365879EFF886C66DD51A5B6A99744B50590C87A23884",
        "C1585EF15A43D875",
        "00FAAC24",
        "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F",
        "F05E231B3894612C49EE000B804EB2A9B8306B508F839D6A5530831D9344AF1C")]
    [InlineData(
        "FF7A617CE69148E4F1726E2F43581DE2AA62D9F805532EDFF1EED687FB54153D",
        "51A51D70A1C11148",
        "001CC5B7",
        "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20212223",
        "EB6C52821D0BBBF7CE7594462ACA4FAAB407DF866569FD07F48CC0B583D6071F1EC0E6B8")]
    public void AesCtrVectors(string key, string iv, string nonce, string plaintext, string ciphertext)
    {
        byte[] keyBytes = Convert.FromHexString(key);
        byte[] ivBytes = Convert.FromHexString(iv);
        byte[] nonceBytes = Convert.FromHexString(nonce);
        byte[] ciphertextBytes = Convert.FromHexString(ciphertext);

        Span<byte> counter = stackalloc byte[16];
        nonceBytes.CopyTo(counter);
        ivBytes.CopyTo(counter.Slice(nonceBytes.Length));
        counter[15] = 1;

        byte[] actual = new byte[ciphertextBytes.Length];
        AesCtr.DecryptCtr(keyBytes, counter, ciphertextBytes, actual);
        Assert.Equal(plaintext, Convert.ToHexString(actual));
    }
}
