Public Class Form2

    ' --- HELPER: Translates "Skyblock Speak" to "Math Speak" ---
    Private Function TranslateToMath(input As String) As String
        ' 1. Lowercase and remove spaces
        input = input.ToLower().Replace(" ", "")

        ' 2. Swap the letters for their math equivalent
        ' k becomes *1000
        ' m becomes *1000000
        ' b becomes *1000000000
        input = input.Replace("k", "*1000")
        input = input.Replace("m", "*1000000")
        input = input.Replace("b", "*1000000000")

        Return input
    End Function

    ' --- THE REAL-TIME CALCULATOR ---
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            ' 1. Get the text from the box
            Dim rawText As String = TextBox1.Text

            ' 2. If it's empty, just reset the label
            If rawText = "" Then
                Label1.Text = "Waiting..."
                Label1.ForeColor = Color.Gray
                Exit Sub
            End If

            ' 3. Translate it (e.g. "1.5k" -> "1.5*1000")
            Dim mathString As String = TranslateToMath(rawText)

            ' 4. THE MAGIC TRICK: Use DataTable to solve the math string
            ' This automatically handles order of operations (BODMAS) and brackets!
            Dim dt As New DataTable()
            Dim result As Object = dt.Compute(mathString, Nothing)

            ' 5. Show the result
            ' Convert to Decimal to format it nicely
            Dim finalNumber As Decimal = Convert.ToDecimal(result)

            ' "N0" adds the commas (e.g. 1,000,000)
            Label1.Text = "= " & finalNumber.ToString("N0")
            Label1.ForeColor = Color.Green

        Catch ex As Exception
            ' If the math is invalid (like "1.5m+"), just show ...
            Label1.Text = "invalid"
            Label1.ForeColor = Color.Red
        End Try
    End Sub

End Class