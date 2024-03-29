using System;
using Repositories;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Models
{
    public class Filme
    {
        [Key]
        public int FilmeId { get; set; }
        [Required]
        public String Nome { get; set; }
        public String DataLancamento { get; set; }
        public String Sinopse { get; set; }
        public double Valor { get; set; }
        public int EstoqueTotal { get; set; }
        public int EstoqueAtual { get; set; }
        public int Locado { get; set; }

        public Filme(){}

        // Construtor da classe
        public Filme(String nome, String dataLancamento, String sinopse, double valor, int estoqueTotal){
            Nome = nome;
            DataLancamento = dataLancamento;
            Sinopse = sinopse;
            Valor = valor;
            EstoqueTotal = estoqueTotal;
            EstoqueAtual = estoqueTotal;
            Locado = 0;

            Context db = new Context();
            db.Filmes.Add(this);
            db.SaveChanges();
        }

         public static void UpdateFilm(int id, String nome, String dataLancamento, String sinopse, double valor, int estoqueTotal){
            Context db = new Context();
            try{
                Filme filme = GetFilme(id);
                filme.Nome = nome;
                filme.DataLancamento = dataLancamento;
                filme.Sinopse = sinopse;
                filme.Valor = valor;
                filme.EstoqueTotal = estoqueTotal;
                try{
                    db.SaveChanges();
                }catch(Exception){
                    //InvalidOperationException
                    //ArgumentException
                    throw new Exception("Não alterou");
                }
            }catch{
                throw new Exception("Filme não encontrado");
            }
        }
        public static void DeleteClient(int id){
            Context db = new Context();
            try{
                Filme filme = db.Filmes.First(filme => filme.FilmeId == id);
                db.Remove(filme);
                try{
                    db.SaveChanges();
                }catch{
                    throw new Exception("Tem locações para este filme");
                }
            }catch{
                throw new Exception("Filme não encontrado");
            }
        }

        // metodo para buscar a lista de todos os filmes
        public static List<Filme> GetFilmes(){
            Context db = new Context();
            IEnumerable<Filme> Filmes = from filmes in db.Filmes select filmes;
            return Filmes.ToList();
        }
        
        // metodo para buscar um filme em específico
        public static Filme GetFilme(int filmeId){
            Context db = new Context();
            return db.Filmes.Find(filmeId);
        }

        // Metodo para dizer que o filme foi locado e contabilizar
        public override string ToString(){
            return  $"Nome: {Nome} \n"+
                    $"Data Lançamento:  {DataLancamento} \n"+
                    $"Sinope: {Sinopse} \n"+
                    $"Valor: R$  {Valor} \nEstoque Atual:  {EstoqueAtual}\n"+
                    //$"Quantidade de locações feitas: {Locado}
                    "\n";
        }  
    }
}
