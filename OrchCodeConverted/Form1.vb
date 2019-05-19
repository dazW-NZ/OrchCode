Imports System.IO
Imports System.Net
Imports System.Xml
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Function g_RBID(ByVal rbServer, ByVal rbName)
        Dim uri = "http://" & rbServer & ":81/Orchestrator2012/Orchestrator.svc/Runbooks"
        Dim wReq As WebRequest = restRequest(uri)
        Dim allParams = wReq

        Dim retObjData = parseXML("/feed/entry/content/properties", "")


    End Function



    Private Function parseXML(ByVal branch, ByVal xmldata)
        Dim doc As New XmlDocument()
        doc.Load(xmldata)
        Dim nodes As XmlNodeList = doc.DocumentElement.SelectNodes(branch)

        'Dim product_id As String = "", product_name As String = "", product_price As String = ""
        Dim dataCollection As New List(Of String)

        For Each node As XmlNode In nodes
            dataCollection = node.SelectSingleNode("Name")
            product_id = node.SelectSingleNode("Product_id").InnerText
            product_name = node.SelectSingleNode("Product_name").InnerText
            product_price = node.SelectSingleNode("Product_price").InnerText
            MessageBox.Show(product_id & " " & product_name & " " & product_price)
        Next
    End Function

    Private Function restRequest(ByVal url As String)
        Try
            Dim uri As Uri = New Uri(url, True)
            Dim request As System.Net.WebRequest = System.Net.WebRequest.Create(uri)
            request.Method = "GET"
            request.Credentials = CredentialCache.DefaultCredentials
            'request.Headers.Add("appkey", "Your Application Key")
            Using response As WebResponse = request.GetResponse()
                Using responsestream As Stream = response.GetResponseStream()
                    Using reader As StreamReader = New StreamReader(responsestream)
                        Dim webresponse As String
                        webresponse = reader.ReadToEnd()
                        Return webresponse
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Private Function webrequestPOST(ByVal url As String, ByVal postdata As String)
        Try
            Dim uri As Uri = New Uri(url, True)
            Dim request As System.Net.WebRequest = System.Net.WebRequest.Create(uri)
            request.Method = "POST"
            request.Credentials = CredentialCache.DefaultCredentials
            request.Headers.Add("appkey", "Your Application Key")
            Dim byteArray() As Byte = Encoding.UTF8.GetBytes(postdata)
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Using dataStream As System.IO.Stream = request.GetRequestStream()
                dataStream.Write(byteArray, 0, byteArray.Length)
            End Using
            Using response As WebResponse = request.GetResponse()
                Using responsestream As Stream = response.GetResponseStream()
                    Using reader As StreamReader = New StreamReader(responsestream)
                        Dim webresponse As String
                        webresponse = reader.ReadToEnd()
                        Return webresponse
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
End Class
