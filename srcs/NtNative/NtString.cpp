#include "NtString.hpp"

NtString::NtString()
{
	buffer_ = { 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00 };
}

NtString::NtString(const std::string& input)
	: NtString()
{
	appendToEnd(input);
}

NtString::NtString(const char* input)
	: NtString()
{
	appendToEnd(input);
}

NtString& NtString::operator+=(const std::string& input)
{
	appendToEnd(input);

	return *this;
}

NtString& NtString::operator+=(const char* input)
{
	appendToEnd(input);

	return *this;
}

unsigned int NtString::length() const
{
	return buffer_.size() - 8;
}

const char* NtString::toString() const
{
	return buffer_.data() + 8;
}

void NtString::appendToEnd(const std::string& input)
{
	for (auto c : input)
	{
		buffer_.push_back(c);
	}

	update();
}

void NtString::update()
{
	*reinterpret_cast<unsigned int*>(buffer_.data() + 4) = length();
}