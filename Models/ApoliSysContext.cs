using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql;

namespace ApoliSys.Models
{
    public partial class ApoliSysContext : DbContext
    {
        public ApoliSysContext()
        {
            //Mapeando Enums para Conversão correta dos tipos
            NpgsqlConnection.GlobalTypeMapper.MapEnum<genero>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<faixa_salarial>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<marca>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<categoria_veiculo>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<combustivel>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<plano_seguro>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<forma_pagamento>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<status_cotacao>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<status_apolice>();
        }

        public ApoliSysContext(DbContextOptions<ApoliSysContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apolice> Apolices { get; set; } = null!;
        public virtual DbSet<Cotacao> Cotacaos { get; set; } = null!;
        public virtual DbSet<Pessoa> Pessoas { get; set; } = null!;
        public virtual DbSet<Segurado> Segurados { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Veiculo> Veiculos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Username=seguradora;Password=seguradoradb;Database=ApoliSys");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Declarando que existem enums
            modelBuilder.HasPostgresEnum<categoria_veiculo>()
                .HasPostgresEnum<combustivel>()
                .HasPostgresEnum<faixa_salarial>()
                .HasPostgresEnum<forma_pagamento>()
                .HasPostgresEnum<genero>()
                .HasPostgresEnum<marca>()
                .HasPostgresEnum<plano_seguro>()
                .HasPostgresEnum<status_apolice>()
                .HasPostgresEnum<status_cotacao>();

            modelBuilder.Entity<Apolice>(entity =>
            {
                entity.ToTable("Apolice");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodigoRamo)
                    .HasMaxLength(4)
                    .HasColumnName("codigo_ramo")
                    .IsFixedLength();

                entity.Property(e => e.DataEmissao).HasColumnName("data_emissao");

                entity.Property(e => e.DataTermino).HasColumnName("data_termino");

                entity.Property(e => e.IdCotacao).HasColumnName("id_cotacao");

                entity.Property(e => e.NumeroApolice)
                    .HasMaxLength(9)
                    .HasColumnName("numero_apolice")
                    .IsFixedLength();

                entity.Property(e => e.ProcessoSusep)
                    .HasMaxLength(20)
                    .HasColumnName("processo_susep");

                entity.Property(e => e.Produto)
                    .HasMaxLength(20)
                    .HasColumnName("produto")
                    .HasDefaultValueSql("'Seguro Veicular'::character varying");

                entity.HasOne(d => d.IdCotacaoNavigation)
                    .WithMany(p => p.Apolices)
                    .HasForeignKey(d => d.IdCotacao)
                    .HasConstraintName("fk_id_cotacao");
            });

            modelBuilder.Entity<Cotacao>(entity =>
            {
                entity.ToTable("Cotacao");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdVeiculo).HasColumnName("id_veiculo");

                entity.Property(e => e.ValorPremioSeguro)
                    .HasColumnType("money")
                    .HasColumnName("valor_premio_seguro");

                entity.Property(e => e.ValorTotalPremioSeguro)
                    .HasColumnType("money")
                    .HasColumnName("valor_total_premio_seguro");

                entity.HasOne(d => d.IdVeiculoNavigation)
                    .WithMany(p => p.Cotacaos)
                    .HasForeignKey(d => d.IdVeiculo)
                    .HasConstraintName("fk_id_veiculo");
            });

            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.ToTable("Pessoa");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bairro)
                    .HasMaxLength(100)
                    .HasColumnName("bairro");

                entity.Property(e => e.Celular)
                    .HasMaxLength(11)
                    .HasColumnName("celular");

                entity.Property(e => e.Cep)
                    .HasMaxLength(8)
                    .HasColumnName("cep");

                entity.Property(e => e.Cidade)
                    .HasMaxLength(100)
                    .HasColumnName("cidade");

                entity.Property(e => e.Cnh)
                    .HasMaxLength(11)
                    .HasColumnName("cnh");

                entity.Property(e => e.Complemento)
                    .HasMaxLength(50)
                    .HasColumnName("complemento")
                    .HasDefaultValueSql("'Não se aplica.'::character varying");

                entity.Property(e => e.CpfCnpj)
                    .HasMaxLength(14)
                    .HasColumnName("cpf_cnpj");

                entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Estado)
                    .HasMaxLength(2)
                    .HasColumnName("estado")
                    .IsFixedLength();

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .HasColumnName("nome");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.Rua)
                    .HasMaxLength(100)
                    .HasColumnName("rua");
            });

            modelBuilder.Entity<Segurado>(entity =>
            {
                entity.ToTable("Segurado");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdPessoa).HasColumnName("id_pessoa");

                entity.Property(e => e.Profissao)
                    .HasMaxLength(50)
                    .HasColumnName("profissao");

                entity.HasOne(d => d.IdPessoaNavigation)
                    .WithMany(p => p.Segurados)
                    .HasForeignKey(d => d.IdPessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_id_pessoa");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdPessoa).HasColumnName("id_pessoa");

                entity.Property(e => e.SenhaUsuario)
                    .HasMaxLength(50)
                    .HasColumnName("senha_usuario");

                entity.HasOne(d => d.IdPessoaNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdPessoa)
                    .HasConstraintName("fk_id_pessoa");
            });

            modelBuilder.Entity<Veiculo>(entity =>
            {
                entity.ToTable("Veiculo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ano).HasColumnName("ano");

                entity.Property(e => e.CodigoFipe)
                    .HasMaxLength(7)
                    .HasColumnName("codigo_fipe")
                    .IsFixedLength();

                entity.Property(e => e.IdSegurado).HasColumnName("id_segurado");

                entity.Property(e => e.Km).HasColumnName("km");

                entity.Property(e => e.Modelo)
                    .HasMaxLength(50)
                    .HasColumnName("modelo");

                entity.Property(e => e.Placa)
                    .HasMaxLength(7)
                    .HasColumnName("placa")
                    .IsFixedLength();

                entity.Property(e => e.Renavam)
                    .HasMaxLength(11)
                    .HasColumnName("renavam")
                    .IsFixedLength();

                entity.Property(e => e.Utilizacao)
                    .HasMaxLength(50)
                    .HasColumnName("utilizacao");

                entity.Property(e => e.ZeroKm).HasColumnName("zero_km");

                entity.HasOne(d => d.IdSeguradoNavigation)
                    .WithMany(p => p.Veiculos)
                    .HasForeignKey(d => d.IdSegurado)
                    .HasConstraintName("fk_id_segurado");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
