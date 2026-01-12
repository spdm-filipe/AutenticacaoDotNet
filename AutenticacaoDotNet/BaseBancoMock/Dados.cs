namespace AutenticacaoDotNet.BaseBancoMock
{
    public static class Dados
    {
        public static List<string> RetornaAcessos()
        {
            var listaAcessos = new List<string>();
            listaAcessos.Add("RH");
            listaAcessos.Add("Admin1");

            return listaAcessos;
        }

    }
}
