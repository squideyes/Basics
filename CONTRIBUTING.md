Although **SquidEyes.Basics** is mostly for the author's own purposes, it should be fairly easy to add additional functionality to the library, if desired.  Just note that a few simple rules will need to be followed when submitting a pull-request:

* Be sure to include a copy of the standard license header in each new source code file you create
  * This can be most conveniently done by using the Visual Studio LicenseHeader extension (once the extension gets updated to work with Visual Studio 2022), by simply invoking the "License Headers" / "Add License Headers To All Files" command on the project(s) containing the updated files.  In the meantime, manually copying an (unchanged!) license header from an existing source file would be your best bet.
* No changes to the solution-wide .editorconfig file will be accepted
* Pull-requests will not be accepted without a full-coverage unit-test for  new or updated functionality
 
NOTE: the basic (pun intended!) idea behind SquidEyes.Basics is to provide a collection of helper classes and extensions that are widely applicable all kinds of applications and with zero out-of-the-box dependencies.  Please try to hew to that intent when making pull-requests.