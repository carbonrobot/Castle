<Query Kind="Program" />

void Main()
{
	var xml = XElement.Load (@"d:\projects\carbonrobot\castle\sandbox\sandcastle\doc\WebTOC.xml");

	var list = Tokenizer.Process(xml);
	list.Dump();
}

public static class Tokenizer 
{
	public static IList<Token> Process(XContainer element){
		var list = new List<Token>();
		foreach(var node in element.Elements()){
			var token = new Token(node.Attribute("Title").Value, node.Attribute("Url").Value);
			token.Tokens = Tokenizer.Process(node);
			list.Add(token);
		}
		return list;
	}
}

public class Token {
	public string Title {get;set;}
	public string Path {get; set;}
	public IList<Token> Tokens {get;set;}
	public Token(string title, string path){
		this.Title = title;
		this.Path = path;
	}
}