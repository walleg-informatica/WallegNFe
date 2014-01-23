﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Trabalhar com o certificado
using System.Security.Cryptography.X509Certificates;

namespace Exemplo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Cria a nota com o objeto "Nota"
            WallegNfe.Nota nota = new WallegNfe.Nota();

            nota.ide.cUF = "35";
            nota.ide.natOp = "Descrição da natureza";
            nota.ide.indPag = "0"; //'0 - a vista, 1 - a prazo, 2 - outros
            nota.ide.mod = "55";
            nota.ide.serie = "1";
            nota.ide.nNF = "12345";
            nota.ide.dEmi = "2014-01-23";
            nota.ide.tpNF = "1";
            nota.ide.cMunFG = "3123";
            nota.ide.tpImp = "1";
            nota.ide.tpEmis = "1";
            nota.ide.cDV = "0";
            nota.ide.tpAmb = "2";
            nota.ide.finNFe = "1";
            nota.ide.procEmi = "3";

            nota.emit.CPF = "11111111111";
            nota.emit.xNome = "Renato Gaucho";
            nota.emit.xLgr = "R. da bavária";
            nota.emit.nro = "123";
            nota.emit.xBairro = "Santa Claus";
            nota.emit.cMun = "131";
            nota.emit.xMun = "Bavária";
            nota.emit.UF = "AC";
            nota.emit.CEP = "12345000";
            nota.emit.cPais = "1058";
            nota.emit.xPais = "BRASIL";
            nota.emit.fone = "13 3123 1231";
            nota.emit.IE = "ISENTO";
            nota.emit.CRT = "123";

            nota.dest.CPF = "11111111111";
            nota.dest.xNome = "Razão de Teste";
            nota.dest.xLgr = "R. Logradouro Teste";
            nota.dest.nro = "123";
            nota.dest.xBairro = "Bairro da Luz";
            nota.dest.cMun = "123";
            nota.dest.xMun = "São Pedro";
            nota.dest.UF = "RR";
            nota.dest.CEP = "12345";
            nota.dest.cPais = "1058";
            nota.dest.xPais = "BRASIL";
            nota.dest.fone = "(13) 3123 3123";
            nota.dest.IE = "ISENTO";
            nota.dest.email = "teste@teste.com";

            WallegNfe.Model.Nota.DUP dup = new WallegNfe.Model.Nota.DUP();
            dup.nDup = "123";
            dup.dVenc = "2014-03-21";
            dup.vDup = "23.33";
            nota.cobr.addDup(dup);


            nota.transp.modFrete = "1";
            nota.transp.xNome = "Transportadora Ficticia";
            nota.transp.IE = "637322284114";
            nota.transp.xEnder = "Rua ficticia, 123";
            nota.transp.xMun = "São Lucas";
            nota.transp.UF = "RO";

            WallegNfe.Model.Nota.DET notaProduto = new WallegNfe.Model.Nota.DET();
            notaProduto.cProd = "123";
            notaProduto.cEAN = "7896090701049";
            notaProduto.xProd = "Produto de teste";
            notaProduto.NCM = "22071090";
            notaProduto.CFOP = "5401";
            notaProduto.uCom = "CX";
            notaProduto.qCom = "10.0000";
            notaProduto.vUnCom = "32.50000000";
            notaProduto.vProd = "325.00";
            notaProduto.cEANTrib = "7896090701049";
            notaProduto.uTrib = "CX";
            notaProduto.qTrib = "10.0000";
            notaProduto.vUnTrib = "32.50000000";
            notaProduto.indTot = "1";


            notaProduto.icms = WallegNfe.Model.Nota.Enum.ICMS.ICMS00;

            
            notaProduto.icms_orig = "1";
            notaProduto.icms_CST = "0";
            notaProduto.icms_modBC = "3";
            notaProduto.icms_vBC = "0";
            notaProduto.icms_pICMS = "0";
            notaProduto.icms_vICMS = "0";
            notaProduto.ipi = WallegNfe.Model.Nota.Enum.IPI.IPI00_49_50_99;
            notaProduto.ipi_CST = "99";
            notaProduto.ipi_vIPI = "0";
            notaProduto.pis = WallegNfe.Model.Nota.Enum.PIS.PIS01_02;
            notaProduto.pis_CST = "01";
            notaProduto.pis_vBC = "0";
            notaProduto.pis_pPIS = "0";
            notaProduto.pis_vPIS = "0";
            notaProduto.cofins = WallegNfe.Model.Nota.Enum.COFINS.CST01_02;
            notaProduto.cofins_CST = "0";
            notaProduto.cofins_vBC = "0";
            notaProduto.cofins_pCOFINS = "0";
            notaProduto.cofins_vCOFINS = "0";
            
            nota.AddDet(notaProduto);


            nota.total.vBC ="0.00";
            nota.total.vICMS = "0.00";
            nota.total.vBCST = "0.00";
            nota.total.vST = "0.00";
            nota.total.vProd = "0.00";
            nota.total.vFrete = "0.00";
            nota.total.vSeg = "0.00";
            nota.total.vDesc = "0.00";
            nota.total.vII = "0.00";
            nota.total.vIPI = "0.00";
            nota.total.vPIS = "0.00";
            nota.total.vCOFINS = "0.00";
            nota.total.vOutro = "0.00";
            nota.total.vNF = "0.00";
            nota.total.vTotTrib = "0.00";


            nota.SalvarNota("C:\\NFE\\teste-nota.xml");

            WallegNfe.Nfe nfe = new WallegNfe.Nfe(false, "C:\\certificado3.pfx");


            //Enviar uma nota
            WallegNfe.Operacao.Recepcao nfeRecepcao = new WallegNfe.Operacao.Recepcao(nfe);

            nfeRecepcao.AdicionarNota(nota);
            String numeroRecibo = nfeRecepcao.Enviar(1).Recibo;

            //Consultar nota
            WallegNfe.Operacao.RetRecepcao nfeRetRecepcao = new WallegNfe.Operacao.RetRecepcao(nfe);
            WallegNfe.Model.Retorno.RetRecepcao retRetorno = nfeRetRecepcao.Enviar(numeroRecibo);

            //Cancelar nota
            WallegNfe.Operacao.RecepcaoEvento nfeRecepcaoEvento = new WallegNfe.Operacao.RecepcaoEvento(nfe);

            Console.ReadLine();
        }
    }
}
