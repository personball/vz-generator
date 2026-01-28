# Jison Parser Port

Written By: Robert Plummer, <RobertLeePlummerJr@gmail.com>  
Description: A jison wrapper that first processes a jison file and then injects a php template with the variables from a .js file.

Base On: <https://github.com/zaach/jison/blob/master/ports/php/readme>

## DIRECTIONS

1. After you've downloaded jison & node.js, navigate in command line interface to the folder that contains the `.jison` file that you want to process

    ```bash
    npm install jison -g
    ```

1. Process the `.jison` file like this

    ```bash
    node csharp.js ./classDiagram.jison 
    mv classDiagram.cs ..
    ```

Here, we use `erDiagram.jison`,`classDiagram.jison` from mermaid source code [v10.6.1](https://github.com/mermaid-js/mermaid/tree/v10.6.1/packages/mermaid/src/diagrams) .

## CONFIGURATION

Configuration takes advantage of the commenting in javascript so as not to conflict with.

- In your `.jison` file
  - that has a `//js` comment BEFORE the javascript line starts a javascript area of the parser section.  Which will be removed from the php.
  - line that simply has `//` ends whatever commenting is currently active.
  - that has a `//php` comment ON the line of php it starts a php area of the parser section.  In Javascript this is just a comment, in php though, the comment is removed so that it can be called.
  - that has no comment, is left alone.
- Comments in the `.jison` file that start with `//option` can be used to configure the output of the php file.
  - `//option optionName:value`
  - Current Options:
    - `namespace` - default is `Jison`
    - `class` - default is your `.jison` file without the file extension
    - `fileName` - default is your `.jison` file without the file extension followed by `.php`

## EXAMPLE

```plain
contents
 : content
	{$$ = $1;} //<--this is left alone
 | contents content
	{
	    //js
		$$ = join($1, $2); //<--this is stripped in the php parser, no action taken in the javascript parser
		
		//php $$ = $SomeClass->someMethod($1, $2); //<-- this is uncommented from the php parser, no action taken in the javascript parser

		/*php
		    $$ = $SomeClass->someMethod($1, $2); //<-- this is uncommented from the php parser, no action taken in the javascript parser
		*/

        /* If you'd like to just strip out the javascript and do nothing else, you can do the following: */
		//js
		    $$ = join($1, $2); //<--this is stripped in the php parser, no action taken in the javascript parser
		//
	}
 ;
```
