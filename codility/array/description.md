<div id="brinza-task-description">
<p>A zero-indexed array A consisting of N integers is given. We are looking for pairs of elements of the array that are equal but that occupy different positions in the array. More formally, a pair of indices (P, Q) is called <i>identical</i> if 0 ≤ P &lt; Q &lt; N and A[P] = A[Q]. The goal is to calculate the number of identical pairs of indices.</p>
<p>For example, consider array A such that:</p>
<tt style="white-space:pre-wrap">  A[0] = 3
A[1] = 5
A[2] = 6
A[3] = 3
A[4] = 3
A[5] = 5</tt>
<p>There are four pairs of identical indices: (0, 3), (0, 4), (1, 5) and (3, 4). Note that pairs (2, 2) and (5, 1) are not counted since their first indices are not smaller than their second.</p>
<p>Write a function:</p>
<blockquote><p style="font-family: monospace; font-size: 9pt; display: block; white-space: pre-wrap"><tt>def solution(a)</tt></p></blockquote>
<p>that, given a zero-indexed array A of N integers, returns the number of identical pairs of indices.</p>
<p>If the number of identical pairs of indices is greater than 1,000,000,000, the function should return 1,000,000,000.</p>
<p>For example, given:</p>
<tt style="white-space:pre-wrap">  A[0] = 3
A[1] = 5
A[2] = 6
A[3] = 3
A[4] = 3
A[5] = 5</tt>
<p>the function should return 4, as explained above.</p>
<p>Assume that:</p>
<blockquote><ul style="margin: 10px;padding: 0px;"><li>N is an integer within the range [<span class="number">0</span>..<span class="number">100,000</span>];</li>
<li>each element of array A is an integer within the range [<span class="number">−1,000,000,000</span>..<span class="number">1,000,000,000</span>].</li>
</ul>
</blockquote><p>Complexity:</p>
<blockquote><ul style="margin: 10px;padding: 0px;"><li>expected worst-case time complexity is O(N*log(N));</li>
<li>expected worst-case space complexity is O(N), beyond input storage (not counting the storage required for input arguments).</li>
</ul>
</blockquote><p>Elements of input arrays can be modified.</p>
</div>
