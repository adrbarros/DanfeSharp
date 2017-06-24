﻿using System;
using System.Collections.Generic;
using System.Drawing;
using DanfeSharp.Graphics;

namespace DanfeSharp.Blocos
{
    internal class TabelaProdutosServicos : ElementoBase
    {
        public CabecalhoBloco CabecalhoBloco { get; private set; }
        public Tabela Tabela { get; private set; }
        public DanfeViewModel ViewModel { get; private set; }

        public TabelaProdutosServicos(DanfeViewModel viewModel, Estilo estilo) : base(estilo)
        {
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            CabecalhoBloco = new CabecalhoBloco(estilo, "DADOS DOS PRODUTOS / SERVIÇOS");

            var ad = AlinhamentoHorizontal.Direita;
            var ac = AlinhamentoHorizontal.Centro;
            var ae = AlinhamentoHorizontal.Esquerda;

            Tabela = new Tabela(Estilo);

            if (ViewModel.IsRetrato)
            { 
                Tabela
                .ComColuna(8.5f, ac, "CÓDIGO PRODUTO")
                .ComColuna(0, ae, "DESCRIÇÃO DO PRODUTO / SERVIÇO")
                .ComColuna(6F, ac, "NCM/SH")
                .ComColuna(3.5F, ac, "O/CST")
                .ComColuna(3.5F, ac, "CFOP")
                .ComColuna(3.1F, ac, "UN")
                .ComColuna(5.3F, ad, "QUANTI.")
                .ComColuna(5.3F, ad, "VALOR", "UNIT.")
                .ComColuna(5, ad, "VALOR", "TOTAL")
                .ComColuna(5, ad, "B CÁLC", "ICMS")
                .ComColuna(5, ad, "VALOR", "ICMS")
                .ComColuna(5, ad, "VALOR", "IPI")
                .ComColuna(3.6F, ad, "ALIQ.", "ICMS")
                .ComColuna(3.6F, ad, "ALIQ.", "IPI");
            }
            else
            {
                Tabela
                .ComColuna(8.1f, ac, "CÓDIGO PRODUTO")
                .ComColuna(0, ae, "DESCRIÇÃO DO PRODUTO / SERVIÇO")
                .ComColuna(5.5F, ac, "NCM/SH")
                .ComColuna(3.1F, ac, "O/CST")
                .ComColuna(3.1F, ac, "CFOP")
                .ComColuna(3F, ac, "UN")
                .ComColuna(5.25F, ad, "QUANTI.")
                .ComColuna(5.6F, ad, "VALOR UNIT.")
                .ComColuna(5.6F, ad, "VALOR TOTAL")
                .ComColuna(5.6F, ad, "B CÁLC ICMS")
                .ComColuna(5.6F, ad, "VALOR ICMS")
                .ComColuna(5.6F, ad, "VALOR IPI")
                .ComColuna(3F, ad, "ALIQ.", "ICMS")
                .ComColuna(3F, ad, "ALIQ.", "IPI");
            }

            Tabela.AjustarLarguraColunas();

            foreach (var p in ViewModel.Produtos)
            {
                var linha = new List<String>
                {
                    p.Codigo,
                    p.DescricaoCompleta,
                    p.Ncm,
                    p.OCst,
                    p.Cfop.Formatar("N0"),
                    p.Unidade,
                    p.ValorUnitario.Formatar(),
                    p.Quantidade.Formatar(),
                    p.ValorTotal.Formatar(),
                    p.BaseIcms.Formatar(),
                    p.ValorIcms.Formatar(),
                    p.ValorIpi.Formatar(),
                    p.AliquotaIcms.Formatar(),
                    p.AliquotaIpi.Formatar()
                };

                Tabela.AdicionarLinha(linha);
            }
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            Tabela.SetPosition(RetanguloTabela.Location);
            Tabela.SetSize(RetanguloTabela.Size);
            Tabela.Draw(gfx);

            CabecalhoBloco.SetPosition(X, Y);
            CabecalhoBloco.Width = Width;
            CabecalhoBloco.Draw(gfx);    
        }


        public RectangleF RetanguloTabela => BoundingBox.CutTop(CabecalhoBloco.Height);
        public Boolean CompletamenteDesenhada => Tabela.LinhaAtual == ViewModel.Produtos.Count;
        public override bool PossuiContono => false;
    }
}