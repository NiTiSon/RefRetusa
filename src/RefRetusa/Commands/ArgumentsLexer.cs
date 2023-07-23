using RefRetusa.Analysis;
using RefRetusa.IO;

namespace RefRetusa.Commands;

public class ArgumentsLexer : Lexer<LiteralNode, string[]>
{
	public void Tokenize(string[] input, in WriteStream<LiteralNode> stream)
	{
		for (int i = 0; i < input.Length; i++)
		{
			string content = input[i];

			int nonMinus = 0;
			int minus = 0;

			for (int j = 0; j < content.Length; j++)
			{
				if (content[j] == '-')
					minus++;
				else
					nonMinus++;
			}

			if (nonMinus > 0 && minus > 0)
				stream.Write(new ArgumentNode(content));
			else
				stream.Write(new ValueNode(content));
		}
	}
}
