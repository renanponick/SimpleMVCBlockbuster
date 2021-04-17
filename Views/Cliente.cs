using System;
using Models;
using Controllers;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Components;

namespace ViewClientes{
    public class ViewCliente{
        // Adiciona todos os clientes - Ok
        public static void AddTodosClientes(){
            ControllerCliente.AddCliente("Junior Rezende", "15/05/1998", "123.123.123-32", 2);
            ControllerCliente.AddCliente("Tafarel Rezende", "12/07/1988", "143.153.123-32", 1);
            ControllerCliente.AddCliente("Bolivar Artagne", "15/05/1978", "123.131.123-32", 3);
            ControllerCliente.AddCliente("Teste Rezende", "15/06/1998", "123.123.123-32", 4);
            ControllerCliente.AddCliente("Rezende", "15/07/1998", "123.123.123-32", 2);
        }
    }
    public class ListagemClientes : Form{
        Form parent;
        Listners listagemClientes;
        ButtonsVoltar buttonVoltar;
        public ListagemClientes(Form parent, int id){
            this.parent = parent;
            this.Text = "Listar Filmes";
            this.Size = new Size(370, 400);
            String[] coluns = {"ID","Nome","Data de Nascimento","Cpf"};
            listagemClientes = new Listners(coluns, 335, 300);
            if(id==0){
                try{
                    IEnumerable funcQuery = from clientes in ControllerCliente.GetClientes() select clientes;
                    foreach (Cliente cliente in funcQuery) {
                        ListViewItem cliente1 = new ListViewItem(cliente.ClienteId.ToString());
                        cliente1.SubItems.Add(cliente.Nome);
                        cliente1.SubItems.Add(cliente.DataNascimento);
                        cliente1.SubItems.Add(cliente.Cpf.ToString());
                        listagemClientes.Items.AddRange(new ListViewItem[]{cliente1});
                    }
                }catch(Exception){
                    ListViewItem filme1 = new ListViewItem("Nenhum Cliente encontrado");
                    listagemClientes.Items.AddRange(new ListViewItem[]{filme1});
                } 
            }else if(id>0){
                try{
                    Cliente cliente = ControllerCliente.GetCliente(id);
                    ListViewItem cliente1 = new ListViewItem(cliente.ClienteId.ToString());
                    cliente1.SubItems.Add(cliente.Nome);
                    cliente1.SubItems.Add(cliente.DataNascimento);
                    cliente1.SubItems.Add(cliente.Cpf.ToString());
                    listagemClientes.Items.AddRange(new ListViewItem[]{cliente1});
                }catch(Exception){
                    ListViewItem cliente1 = new ListViewItem("Nenhum Cliente encontrado");
                    listagemClientes.Items.AddRange(new ListViewItem[]{cliente1});
                } 
            }
            buttonVoltar = new ButtonsVoltar(this.Width/3+50, listagemClientes.Height+10, new System.EventHandler(this.Voltar));
            this.Controls.Add(listagemClientes);
            this.Controls.Add(buttonVoltar);
        }
        public void Voltar(object sender, EventArgs args){
            this.parent.Show();
            this.Close();
        }
    }
    public class GetCliente : Form{
        Form parent;
        ButtonsVoltar buttonVoltar;
        ButtonsBuscar buttonsBuscar;
        LabelPadrao labelId;
        InputPadrao inputId;
        public GetCliente(Form parent){
                this.parent = parent;
                this.Text = "Buscar Cliente";
                this.Height = 250;
                labelId = new LabelPadrao("Digite o ID do Cliente:", 200, 5, 30);
                inputId = new InputPadrao(this.Width-30, 5, 70);

                buttonVoltar = new ButtonsVoltar(this.Width/3-20, 100, Voltar);
                buttonsBuscar = new ButtonsBuscar(this.Width/3+40, 100, Buscar);

                this.Controls.Add(labelId);
                this.Controls.Add(inputId);
                this.Controls.Add(buttonVoltar);
                this.Controls.Add(buttonsBuscar);
        }
        public void Voltar(object sender, EventArgs args){
            this.parent.Show();
            this.Close();
        }
        public void Buscar(object sender, EventArgs args){
            int id = Convert.ToInt32(this.inputId.Text);
            new ListagemClientes(this, id).Show();
            this.Hide();
        }
    }
     public class AddCliente : Form{
        Form parent;
        ButtonsVoltar buttonVoltar;
        ButtonsSalvar buttonSalvar;
        LabelPadrao labelNome;
        LabelPadrao labelNascimento;
        LabelPadrao labelCpf;
        LabelPadrao labelDiasDev;
        InputPadrao inputNome;
        InputMascarado inputNascimento;
        InputMascarado inputCpf;
        InputMascarado inputDiasDev;
        public AddCliente(Form parent){
                this.parent = parent;
                this.Text = "Adicionar Cliente";
                this.Height = 400;
                
                labelNome = new LabelPadrao("Digite o Nome:", 200, 5, 10);
                inputNome = new InputPadrao(this.Width-30, 5, 30);

                labelNascimento = new LabelPadrao("Digite a data de nascimento:", 200, 5, 60);
                inputNascimento = new InputMascarado(this.Width-30, 5, 90, "99/99/9999");
                
                labelCpf = new LabelPadrao("Descreva o CPF:", 200, 5, 120);
                inputCpf = new InputMascarado(this.Width-30, 5, 150, "999,999,999-99");

                labelDiasDev = new LabelPadrao("Digite a quantidade de dias para devolução:", 200, 5, 180);
                inputDiasDev = new InputMascarado(this.Width-30, 5, 210, "09");

                buttonVoltar = new ButtonsVoltar(this.Width/3-20, 250, Voltar);
                buttonSalvar = new ButtonsSalvar(this.Width/3+40, 250, Salvar);

                this.Controls.Add(labelNome);
                this.Controls.Add(labelNascimento);
                this.Controls.Add(labelCpf);
                this.Controls.Add(labelDiasDev);
                
                this.Controls.Add(inputNome);
                this.Controls.Add(inputNascimento);
                this.Controls.Add(inputDiasDev);
                this.Controls.Add(inputCpf);
                
                this.Controls.Add(buttonVoltar);
                this.Controls.Add(buttonSalvar);
        }
        public void Voltar(object sender, EventArgs args){
            this.parent.Show();
            this.Close();
        }
        public void Salvar(object sender, EventArgs args){
            try{
                int dias = Convert.ToInt32(this.inputDiasDev.Text);
                ControllerCliente.AddCliente(this.inputNome.Text, 
                                            this.inputNascimento.Text,
                                            this.inputCpf.Text, 
                                            dias);
                MessageBox.Show(
                    "Dados Cadastrados",
                    "Informação",
                    MessageBoxButtons.OK);                        
                this.parent.Show();
                this.Close();
            }catch(Exception){
                MessageBox.Show(
                    "Preencher os dados corretamente",
                    "Informação",
                    MessageBoxButtons.OK);
            }
        }
    }
}
           