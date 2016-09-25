#!/usr/bin/env ruby

class NilSubstring
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

  def longest_valid
    string
      .split(/\d/)
      .sort { |a, b| b.length <=> a.length }
      .find (-> { NilSubstring.new }) { |item| /[a-z]/ =~ item && /[A-Z]/ =~ item }
  end
end

def solution(string)
  Password.new(string).longest_valid.length
end
