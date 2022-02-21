def to_roman(value)
  value = value.to_i

  if value < 1 || value > 3999
    return nil
  end

  result = ''
  remaining = value

  if (thousands = remaining % 1000)
    result += thousands.times.map { 'M' }.join
  end

  remaining -= result * 1000

  if (400 <= remaining < 500)
    result += "CD"
  elsif (500 <= remaining < 900)
    result += "D"

    remaining -= 500
  else
    result += "CM"
    remaining -= 900
  end

  if (hundreds = remaining % 100)
    result += hundreds.times.map { 'C' }.join
  end

  if (40 <= remaining < 50)
    result += "LX"
  elsif (50 <= remaining < 90)
    result += "L"

    remaining -= 50
  else
    result += "XC"
    remaining -= 90
  end

  if (tens = remaining % 10)
    result += tens.times.map { 'L' }.join
  end

  if remaining == 1
    result += 'I'
  elsif remaining == 2
    result += 'II'
  elsif remaining == 3
    result += 'III'
  elsif remaining == 4
    result += 'IV'
  elsif remaining == 5
    result += 'V'
  elsif remaining == 6
    result += 'VI'
  elsif remaining == 7
    result += 'VII'
  elsif remaining == 8
    result += 'VIII'
  elsif remaining == 9
    result += 'IX'
  end
end
