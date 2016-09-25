#!/usr/bin/env ruby

class NilValidSubstring
  def nil?
    true
  end

  def length
    -1
  end
end

class Password
  attr_reader :string

  def initialize(string)
    @string = string.to_s.strip
  end

  def each_valid
    (start_index..end_index).each do |i|
      (i..end_index).to_a.reverse.each do |j|
        substring = string[i..j]

        if (/\d/ =~ substring)
          next
        end

        if (/[a-z]/ =~ substring && /[A-Z]/ =~ substring)
          yield substring
        end
      end
    end
  end

  def longest_valid
    longest = NilValidSubstring.new
    each_valid do |valid|
      longest = valid if valid.length > longest.length
    end

    longest
  end

  private

  def start_index
    0
  end

  def end_index
    string.length - 1
  end
end

def solution(string)
  Password.new(string).longest_valid.length
end
