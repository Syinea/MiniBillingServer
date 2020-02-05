# Mini Billing Server

The leaked server files require an IIS server and some ASP pages to access the silk-table of the Database. The IIS is a huge bloatware for this small task. This small programm emulates a billing server.

This program is aimed for primary for developers and not for productive usage.

## Setup

* Change the file config.ini.dist in Settings folder to config.ini at the same folder and set up your settings.
* Compile
* and run
* Set your billing server url to the address specified in the settings (also, you may just check the console window to see the address listening on)


## Extend

It works for basic silk transactions. I don't know which features are required to make it a full featured billing server.

Each URL should be handled in its own Handler, derived from `IHttpHandler`. Try to keep the code clean by placing database-related stuff into the `SilkDB.cs`.


## License

Free for everyone to do anything. Change or reuse what ever you like. You may even sell it. But that would not be fair for the buyer ...


## Rights

florian0 Coder

Syinea rewrite

