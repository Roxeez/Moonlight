#ifndef NTCORE_NTSTRING_HPP
#define NTCORE_NTSTRING_HPP

#include <string>
#include <vector>

class NtString
{
public:
	NtString();
	NtString(const std::string& input);
	NtString(const char* input);

	NtString& operator+=(const std::string& input);
	NtString& operator+=(const char* input);

	unsigned int length() const;
	const char* toString() const;

private:
	std::vector<char> buffer_;

	void appendToEnd(const std::string& input);
	void update();
};

#endif //NTCORE_NTSTRING_HPP

