using ZH2_NY905R.Moduls;

namespace ZH2_NY905R
{
    public partial class Form1 : Form
    {
        OrderContext context=new OrderContext();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CustomerListing();
            ProductListing();
            ShowOrdersData();
        }

        private void textBoxCustomer_TextChanged(object sender, EventArgs e)
        {
            CustomerListing();
        }

        private void CustomerListing()
        {
            var listedCustomers = from x in context.Customers
                                  where x.FullName.Contains(textBoxCustomer.Text)
                                  select new CustomerSimple
                                  {
                                      ID = x.CustomerPk,
                                      Name=x.FullName
                                  };

            listBoxCustomer.DataSource = listedCustomers.ToList();
            listBoxCustomer.DisplayMember = "Name";
        }

        private void textBoxProduct_TextChanged(object sender, EventArgs e)
        {
            ProductListing();
        }

        private void ProductListing()
        {
            var listedProducts = from x in context.Products
                                 where x.Name.Contains(textBoxProduct.Text)
                                 select x;

            listBoxProduct.DataSource = listedProducts.ToList();
            listBoxProduct.DisplayMember = "Name";
        }

        private void listBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowOrdersData();

        }

        private void ShowOrdersData()
        {
            var selectedCustomer = (CustomerSimple)listBoxCustomer.SelectedItem;
            var CustomerOrders = from x in context.Orders
                                 where x.CustomerFk == selectedCustomer.ID
                                 select new OrderDetails
                                 {
                                     ID = x.OrderPk,
                                     CustomerName = x.CustomerFkNavigation.FullName,
                                     Company = x.CustomerFkNavigation.Company,
                                     Product = x.ProductFkNavigation.Name,
                                     Quantity = x.Quantity,
                                     OrderPrice = (x.Quantity * x.ProductFkNavigation.UnitPrice)
                                 };
            orderDetailsBindingSource.DataSource = CustomerOrders.ToList();
            var sumOfCustomer = (from x in context.Orders
                                 where x.CustomerFk == selectedCustomer.ID
                                 select x.ProductFkNavigation.UnitPrice * x.Quantity).Sum();
            textBoxSum.Text = sumOfCustomer.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(MessageBox.Show("Do you want to quit the program?", "Closing", MessageBoxButtons.OKCancel) == DialogResult.OK)) e.Cancel = true;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete the current order?","Deleting",MessageBoxButtons.OKCancel)==DialogResult.OK)
            {
                var CurrentOrder = (OrderDetails)orderDetailsBindingSource.Current;
                var OrderToBeDeleted = (from x in context.Orders
                                        where x.OrderPk == CurrentOrder.ID
                                        select x).FirstOrDefault();

                context.Orders.Remove(OrderToBeDeleted);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ShowOrdersData();
            }

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var selectedCustomer = (CustomerSimple)listBoxCustomer.SelectedItem;
            var selectedProduct = (Product)listBoxProduct.SelectedItem;
            int q = 0;
            if(int.TryParse(textBoxQuantity.Text, out q) && q != 0)
            {
                Order orderToBeAdded = new Order();

                orderToBeAdded.CustomerFk = selectedCustomer.ID;
                orderToBeAdded.ProductFk = selectedProduct.ProductPk;
                orderToBeAdded.Quantity = q;

                context.Orders.Add(orderToBeAdded);


                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                ShowOrdersData();
            }
            else MessageBox.Show("Quantity should be a number!", "Wrong input");

        }

        private void buttonCustomer_Click(object sender, EventArgs e)
        {
            FormCustomer form = new FormCustomer();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Customer c = new Customer();
                c.FullName = form.textBox1.Text;
                c.Company = form.textBox2.Text;

                context.Customers.Add(c);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                CustomerListing();
            }
        }

        private void buttonProduct_Click(object sender, EventArgs e)
        {
            FormProduct form = new FormProduct();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Product p = new Product();

                p.Name=form.textBox1.Text;
                p.UnitName = form.listBox1.SelectedItem.ToString();
                p.UnitPrice = form.price;

                context.Products.Add(p);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ProductListing();
            }
        }
    }
}