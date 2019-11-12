using Microsoft.EntityFrameworkCore;
using senai_2s2019_CodeXP_Gufos.Domains;

namespace senai_2s2019_CodeXP_Gufos.Contexts
{
    public partial class GufosContext : DbContext
    {
        public GufosContext()
        {
        }

        public GufosContext(DbContextOptions<GufosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<Localizacao> Localizacao { get; set; }
        public virtual DbSet<Presenca> Presenca { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-NJ6LHN1\\SQLDEVELOPER; Database=Gufos; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasIndex(e => e.Titulo)
                    .HasName("UQ__Categori__7B406B56D4C0E229")
                    .IsUnique();

                entity.Property(e => e.CategoriaId).HasColumnName("Categoria_id");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.Property(e => e.EventoId).HasColumnName("Evento_id");

                entity.Property(e => e.AcessoLivre)
                    .HasColumnName("Acesso_livre")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CategoriaId).HasColumnName("Categoria_id");

                entity.Property(e => e.DataEvento)
                    .HasColumnName("Data_evento")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocalizacaoId).HasColumnName("Localizacao_id");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.CategoriaId)
                    .HasConstraintName("FK__Evento__Categori__45F365D3");

                entity.HasOne(d => d.Localizacao)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.LocalizacaoId)
                    .HasConstraintName("FK__Evento__Localiza__47DBAE45");
            });

            modelBuilder.Entity<Localizacao>(entity =>
            {
                entity.HasIndex(e => e.Cnpj)
                    .HasName("UQ__Localiza__AA57D6B471386BC9")
                    .IsUnique();

                entity.HasIndex(e => e.RazaoSocial)
                    .HasName("UQ__Localiza__7DD02876932694E6")
                    .IsUnique();

                entity.Property(e => e.LocalizacaoId).HasColumnName("Localizacao_id");

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("CNPJ")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasColumnName("Razao_social")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Presenca>(entity =>
            {
                entity.Property(e => e.PresencaId).HasColumnName("Presenca_id");

                entity.Property(e => e.EventoId).HasColumnName("Evento_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioId).HasColumnName("Usuario_id");

                entity.HasOne(d => d.Evento)
                    .WithMany(p => p.Presenca)
                    .HasForeignKey(d => d.EventoId)
                    .HasConstraintName("FK__Presenca__Evento__4AB81AF0");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Presenca)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK__Presenca__Usuari__4BAC3F29");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.ToTable("Tipo_usuario");

                entity.HasIndex(e => e.Titulo)
                    .HasName("UQ__Tipo_usu__7B406B568DC7284B")
                    .IsUnique();

                entity.Property(e => e.TipoUsuarioId).HasColumnName("Tipo_usuario_id");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Usuario__A9D10534CA5E97CE")
                    .IsUnique();

                entity.HasIndex(e => e.Nome)
                    .HasName("UQ__Usuario__7D8FE3B256B5CCE5")
                    .IsUnique();

                entity.Property(e => e.UsuarioId).HasColumnName("Usuario_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TipoUsuarioId).HasColumnName("Tipo_usuario_id");

                entity.HasOne(d => d.TipoUsuario)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.TipoUsuarioId)
                    .HasConstraintName("FK__Usuario__Tipo_us__3C69FB99");
            });
        }
    }
}
