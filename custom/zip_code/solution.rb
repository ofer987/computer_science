class ZipCode
  attr_reader :a, :b

  def initialize(a, b)
    @a = a.to_s
    @b = b.to_s
  end

  def to_i
    to_s.to_i
  end

  def to_s
    str = ""
    each_char { |ch| str += ch }

    str
  end

  def each_char
    (0..longest_length).each do |index|
      yield a[index] unless a[index].nil?
      yield b[index] unless b[index].nil?
    end
  end

  private

  def longest_length
    a.length > b.length ? a.length : b.length
  end

  def foo(str, index)
    str[index]
  end
end

def solution(a, b)
  a = Integer(a).to_s
  b = Integer(b).to_s


  ZipCode.new(a, b).to_i
end
