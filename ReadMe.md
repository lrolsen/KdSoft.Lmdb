## Synopsis

At the top of the file there should be a short introduction and/ or overview that explains **what** the project is. This description should match descriptions added for package managers (Gemspec, package.json, etc.)

## Code Example

Show what the library does as concisely as possible, developers should be able to figure out **how** your project solves their problem by looking at the code example. Make sure the API you are showing off is obvious, and that your code is short and concise.

## Motivation

A short description of the motivation behind the creation and maintenance of the project. This should explain **why** the project exists.

## Installation

Provide code examples and explanations of how to get the project.

### Installing the native libraries
* Windows: A recent x64 build is included in this project.

* Linux-like: 

  * Install package, Example for Ubuntu: `sudo apt-get install liblmdb-dev`
  * Install from source, like in this example

  ```
  git clone https://github.com/LMDB/lmdb
  cd lmdb/libraries/liblmdb
  make && make install
  ```

## API Reference

Depending on the size of the project, if it is small and simple enough the reference docs can be added to the README. For medium size to larger projects it is important to at least provide a link to where the API reference docs live.

## Tests

On non-Windows platforms, LMDB must already be installed so that it can be looked up
by DLLImport using the platform-typical name.

## Contributors

Let people know how they can dive into the project, include important links to things like issue trackers, irc, twitter accounts if applicable.

## License

A short snippet describing the license (MIT, Apache, etc.)