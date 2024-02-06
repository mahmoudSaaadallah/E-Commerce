// this var used to creat a builder for web application
using E_Commerce.DataAccess.Data;
using E_Commerce.DataAccess.Repository;
using E_Commerce.DataAccess.Repository.IRepository;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

// The next line add the the services of MVC.
// Add services to the container.
builder.Services.AddControllersWithViews();


// here we will put the configration that will allow to this programe to use the sql which its
//   connectionstring inside the appsettings.jason file.
// "ConnectionStrings": {
//  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB; Database=OnlineBookShop; Trusted_Connection=True"}
// This connection string is the connection to database.
// The connection string contain some important members:
// key which is the name of connect which could be any thing in this example it called DefaultConnection
// then Value that contain mulite things like:
// Server: Which used to store the name of server that I will connect to, in this example i will connect in
//     the local server in my sqlserver whoes name is (localdb)\\MSSQLLocalDB
// Database: Which used to store the name of database that will be created.
// Trused_Connection: which define the Trused for your connection
// TrustServerCertificate: the transport layer will use SSL to encrypt the channel and bypass walking the
//    certificate chain to validate trust. If TrustServerCertificate is set to true and encryption is
//    turned on, the encryption level specified on the server will be used even if Encrypt is set to false.
//    The connection will fail otherwise.

// Now back to Builder to add teh service that AddingDbContext which is responsable for connection to sql
//   using EntityFramework.
builder.Services.AddDbContext<ApplicationDbContext>(_context => _context.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// this line of code start bulit the app.
var app = builder.Build();


// The Development variable here is the same variable that already used in the launchSettings.json file in the Properties folder.
// IsDevelopment is a building helper. 
// The developer exception page is enabled by default and provides helpful information on exceptions.
// Production apps should not be run in development mode because the developer exception page can leak
// sensitive information.
// The following code sets the exception endpoint to /Error and enables HTTP Strict Transport Security
//  Protocol (HSTS) when the app is not running in development mode:
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();//Redirects HTTP requests to HTTPS
app.UseStaticFiles();//Enables static files, such as HTML, CSS, images, and JavaScript to be served which are in wwwroot. and any other files.

app.UseRouting();//Adds route matching to the middleware pipeline.

app.UseAuthorization();//Authorizes a user to access secure resources


// In the following Method we add the default Controller that will run when the programe start.
// The meaning of the Route is the pattern that the pages apear in Mvc as the Mvc has a specific pattern 
//   which is    <controller>/<Action or View>/<Id or Model>.
// The Id is optional and if we don't add any Action it will use the Index as a default Action.
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();//  Runs the app.


// In the Controllers folder we have all our controllers that we need in our app 
//   all the Controllers files must be ended with Controller keyword like HomeController which already inside the Controllers folder.
// Also the View folder must contain all the view files for each controller 
//   we will find that inside the Views Folder we will find another folders that has the name exaclty like the controller that is connected with
//   so we have to know that inside the views folder we have to name the folders with the name of there controllers.
//   like for example we have a subfolder inside the views folder called Home which connect withe the HomeController.


// When we run the programe we find that the result page is not the Index from the Home only it also has so
//   extra feature that are not inside the Index.cshtml Action file like the header of the page and the
//   footer of the page they are not included in this file, but If we go to Views folder we will find that
//   there is another folder called Shared this folder contain _Layout.cshtml Action file this file is 
//   applied by default to all the action files.
// Now if we look inside the _Layout.cshtml file we will find the header that already applied and also It
//   has the footer that applied, to know how this file applied by default for all the Action files then we
//   have to look at two thing the first one is the name of the file which start with underscore this mean
//   this file is a parent file. Second thing if we look inside this file we will find that between the
//   header and footer ther is a @RenderBody() method this mean In a Razor layout page, renders the portion
//   of a content page that is not within a named section. which also mean any Action file will be as body
//   for this file.
// Also we will find another method called  @await RenderSectionAsync("Scripts", required: false) which mean
//  In layout pages, asynchronously renders the content of the section named <paramref name="name"/>.
//
// <param name="name">The section to render.</param>
// <param name="required">Indicates the <paramref name="name"/> section must be registered
// (using <c>@section</c>) in the page.</param>
// <returns>
// A <see cref="Task{HtmlString}"/> that on completion returns an empty <see cref="IHtmlContent"/>.
// </returns>
// <remarks>The method writes to the <see cref="RazorPageBase.Output"/> and the value returned is a token
// value that allows the Write (produced due to @RenderSection(..)) to succeed. However the
// value does not represent the rendered content.</remarks>
// <exception cref="InvalidOperationException">if <paramref name="required"/> is <c>true</c> and the section
// was not registered using the <c>@section</c> in the Razor page.</exception>

// Also Inside the Views/Shared folder we will find _ValidationScriptsPartial.cshtml file as we see this
//   file also start with underscore which mean it's a parent file, and also we will find his name contaion
//   Partial which mean this file can't run by alone by himself it must run inside another main view.

// So the question now how the MVC know that _Layout.cshtml file is the master page of the application?
//   Ans => Inside the Views folder ther is another file _ViewStart.cshtml which contain the the definetion
//   of the Master page Layout = "_Layout"; by this line of code inside this file the MVC could know the
//   master file.

// Inside the Views/Shard folder there is another file called _ViewImports.cshtml which is a globel import
//   file that contain the important Import NameSpaces, rather to use import at each file we could add them in this file, and all the other file will use the same import.  



// Now going to the appsettings.json which used to store configuration settings such as database connection
//   strings, any application scope global variables, etc.




// To creat a connection to database using SqlServer then it will be better if make this connection inside 
//     appsettings.json file.
//  "ConnectionStrings": {
//  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB; Database=OnlineBookShop; Trusted_Connection=True;
//  TrustServerCertificate=True"}
// This is my connection string.
// The connection string contain some important members:
// key which is the name of connect which could be any thing in this example it called DefaultConnection
// then Value that contain mulite things like:
// Server: Which used to store the name of server that I will connect to, in this example i will connect in
//     the local server in my sqlserver whoes name is (localdb)\\MSSQLLocalDB
// Database: Which used to store the name of database that will be created.
// Trused_Connection: which define the Trused for your connection
// TrustServerCertificate: the transport layer will use SSL to encrypt the channel and bypass walking the
//    certificate chain to validate trust. If TrustServerCertificate is set to true and encryption is
//    turned on, the encryption level specified on the server will be used even if Encrypt is set to false.
//    The connection will fail otherwise.



// Now after connecting to database and adding the Category table to database lets now add a controller for 
//   the Category
// So we will go to Controller folder and add a new Controller with name CategoryController.cs
// Not that all the file's name in Controller folder must end with Controller Keyword.

// Then now after adding the controller we have to add its action(View), So we have to go to Views folder
//   and make a new folder inside it with the same name of controller Category.
// Inside the Views/Category folder we will creat new view file with name Index.cshtml

// Now after creating the view to Category then we want to add the Category page refernce or link in the nav par
//    that already defined in _Layout.cshtml file, so we will go there and add Category link.

// Then now lets go to application DbContext to add some data to Category using OnModelCreating method.

// After adding data to Category table we want to display this data inside the category page in web.
// To do so we have to back again to CategoryController.cs to add the Dbcontext object who will access
//   database and get data, 
// Now after creating the object lets get data inside the IActionREsult Index method to have the
//   ability to use this data in the UI view file.