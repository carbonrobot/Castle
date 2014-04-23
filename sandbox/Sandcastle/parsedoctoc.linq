<Query Kind="Program" />

void Main()
{
	var xml = XElement.Load (@"d:\projects\sandbox\sandcastle\doc\WebTOC.xml");
	
	var sb = new StringBuilder();
	Parser.Loop(sb, xml);
	sb.ToString().Dump();
}

public static class Parser 
{
	static bool formatted = true;
	static char newline = '\n';
	static char tab = ' ';
	static string linkFormat = "<li class=\"list-group-item\"><a href=\"{0}\">{1}</a></li>";
	
	public static void Loop(StringBuilder sb, XContainer element, int depth = 1){
		sb.Append("<ul class=\"list-group\">");
		sb.Append(newline);
		foreach(var node in element.Elements()){
			sb.Append(tab, depth);
			sb.AppendFormat(linkFormat, node.Attribute("Url").Value, node.Attribute("Title").Value);
			sb.Append(newline);
			
			if(node.HasElements){
				sb.Append(tab, depth);
				sb.Append("<li class=\"list-group-item\">");
				sb.Append(newline);
				
				depth++;
				sb.Append(tab, depth);
				Loop(sb, node, ++depth);
				sb.Append(tab, --depth);
				sb.Append("</li>");
				sb.Append(newline);
			}
		}
		sb.Append(tab, --depth);
		sb.Append("</ul>");
		sb.Append(newline);
	}
}
