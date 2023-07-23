using RefRetusa.IO;

namespace RefRetusa.Analysis;

public interface Lexer<Token, Input>
{
	public void Tokenize(Input input, in WriteStream<Token> output);
}